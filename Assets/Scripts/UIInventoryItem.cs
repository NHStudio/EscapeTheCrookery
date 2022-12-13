using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector]
    public Transform parentAfterDrag;
    
    [SerializeField] private ItemsMeta.Item storedItem = ItemsMeta.Item.Null;

    public UIInventorySlot currParent;

    public ItemsMeta.Item StoredItem
    {
        get => storedItem;
        set
        {
            storedItem = value;
            if (_image is null)
            {
                return;
            }
            
            if (storedItem != ItemsMeta.Item.Null)
            {
                _image.sprite = ItemsMeta.ItemsDesc[storedItem].Icon;
            }
            else
            {
                _image.sprite = null;
            }
        }
    }
    
    private Image _image;

    public void Start()
    {
        _image = GetComponent<Image>();
        // Apply sprite for item set in Inspector
        StoredItem = StoredItem;

        currParent = GetComponentInParent<UIInventorySlot>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var transform1 = transform;
        parentAfterDrag = transform1.parent;
        transform.SetParent(transform1.root);
        transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        currParent = GetComponentInParent<UIInventorySlot>();
        _image.raycastTarget = true;

        if (!eventData.hovered.Contains(InventoryManager.Instance.inventoryUI))
        {
            currParent.DropItem();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = Color.cyan;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            currParent.UseItem();
        }
    }
}
