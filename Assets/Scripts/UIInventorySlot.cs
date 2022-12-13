using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour, IDropHandler
{
    public int slotIndex = 0;
    public bool isEquipmentSlot = false;
    public GameObject inventoryItemPrefab;

    private Image _image;

    private GameObject _childItem;
    private UIInventoryItem _childItemUIComponent;
    
    private GameObject _player;
    private PlayerController _playerController;

    private List<ItemsMeta.Item> itemList;

    public void Start()
    {
        _image = GetComponent<Image>();
        InventoryManager.Instance.OnInventoryLayoutChange += OnInventoryLayoutChange;

        if (slotIndex >= InventoryManager.Instance.Items.Count)
        {
            _image.color = Color.gray;
            this.enabled = false;
        }
        
        _player = GameObject.FindWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();

        itemList = isEquipmentSlot ? InventoryManager.Instance.Weapons : InventoryManager.Instance.Items;
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (itemList[slotIndex] != ItemsMeta.Item.Null)
        {
            return;
        }

        GameObject dropped = eventData.pointerDrag;
        UIInventoryItem uiItem = dropped.GetComponent<UIInventoryItem>();
        
        if (itemList == InventoryManager.Instance.Weapons &&
            uiItem.currParent.itemList == InventoryManager.Instance.Items)
        {
            // Forbid non-weapons to be placed in Equipment slots
            BaseItemDesc itemDesc = InventoryManager.Instance.GetItemDesc(uiItem.currParent.slotIndex);
            if (!itemDesc.IsWeapon) return;
        }

        ItemsMeta.Item item = uiItem.currParent.itemList == InventoryManager.Instance.Weapons
            ? InventoryManager.Instance.TakeWeapon(uiItem.currParent.slotIndex, false)
            : InventoryManager.Instance.TakeItem(uiItem.currParent.slotIndex, false);
        
        uiItem.currParent._childItem = null;
        _childItem = dropped;
        _childItemUIComponent = _childItem.GetComponent<UIInventoryItem>();
        uiItem.parentAfterDrag = transform;

        if (itemList == InventoryManager.Instance.Weapons)
        {
             InventoryManager.Instance.PutWeapon(item, slotIndex);
        } else if (itemList == InventoryManager.Instance.Items)
        {
             InventoryManager.Instance.PutItem(item, slotIndex);
        }
    }

    // Called by child item on double-click after use
    public void UseItem()
    {
        BaseItemDesc desc = ItemsMeta.ItemsDesc[_childItemUIComponent.StoredItem];
        if (desc.IsWeapon)
        {
            // Weapons are only equippable
            return;
        }
            
        desc.Use();
        if (!desc.OneTimeUsable) return;
        
        // One-time usable
        InventoryManager.Instance.TakeItem(slotIndex);
        Destroy(_childItem);
        _childItem = null;
        _childItemUIComponent = null;
    }
    
    public void DropItem()
    {
        Destroy(_childItem);
        _childItem = null;
        _childItemUIComponent = null;
        
        ItemsMeta.Item itemToDrop = itemList == InventoryManager.Instance.Weapons
                    ? InventoryManager.Instance.TakeWeapon(slotIndex)
                    : InventoryManager.Instance.TakeItem(slotIndex);
        
        Debug.Log($"Dropped item: {itemToDrop}");
        _playerController.DropItem(itemToDrop);
    }
    
    private void OnInventoryLayoutChange()
    {
        // Inventory extension
        if (slotIndex < itemList.Count)
        {
            _image.color = Color.white;
            this.enabled = true;
        }
        else
        {
            return;
        }

        if (itemList[slotIndex] != ItemsMeta.Item.Null && _childItem is null)
        {
            _childItem = Instantiate(inventoryItemPrefab, transform);
            _childItemUIComponent = _childItem.GetComponent<UIInventoryItem>();
            _childItemUIComponent.currParent = this;
            _childItemUIComponent.StoredItem = itemList[slotIndex];
        } else if (itemList[slotIndex] == ItemsMeta.Item.Null && _childItem is not null)
        {
            Destroy(_childItem);
            _childItem = null;
            _childItemUIComponent = null;
        }
    }
}
