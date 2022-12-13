using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public List<ItemsMeta.Item> Weapons;
    public List<ItemsMeta.Item> Items;

    public GameObject inventoryUI;

    public static InventoryManager Instance { get; private set; }

    public event Action OnInventoryLayoutChange;

    private bool _oneShot = true;

    public void Awake()
    {
        Instance = this;
        ItemsMeta.ItemsDesc = new()
        {
            { ItemsMeta.Item.Gun, GunItemDesc.Instance },
            { ItemsMeta.Item.Shotgun, ShotgunItemDesc.Instance },
            { ItemsMeta.Item.AidKit, AidKitItemDesc.Instance }
        };
    }
    
    public void Update()
    {
        if (_oneShot)
        {
            inventoryUI.SetActive(false);
            _oneShot = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) Use(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Use(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Use(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) Use(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) Use(4);

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            if (inventoryUI.activeSelf)
            {
                // Force UI update
                OnInventoryLayoutChange();
            }
        }
    }

    public void Extend() {
        Items.Add(ItemsMeta.Item.Null);
    }

    public bool Store(ItemsMeta.Item item)
    {
        BaseItemDesc itemDesc = ItemsMeta.ItemsDesc[item];

        if (itemDesc.IsWeapon)
        {
            for (ItemsMeta.WeaponSlot slot = 0; (int)slot < Weapons.Count; ++slot)
            {
                int slotIdx = (int)slot;
                if (Weapons[slotIdx] == ItemsMeta.Item.Null) {
                    PutWeapon(item, slotIdx);
                    Debug.Log($"Equipped {item} to slot {slotIdx}");
                    return true;
                }
            }
        }
        
        for (int i = 0; i < Items.Count; ++i) {
            if (Items[i] == ItemsMeta.Item.Null) {
                PutItem(item, i);
                return true;
            }
        }

        return false;
    }
    
    public void PutWeapon(ItemsMeta.Item item, int slotIdx, bool notify = true)
    {
        Debug.Assert(slotIdx < Weapons.Count);
        Debug.Assert(Weapons[slotIdx] == ItemsMeta.Item.Null);
        Debug.Assert(item != ItemsMeta.Item.Null);
        
        Weapons[slotIdx] = item;
        
        BaseItemDesc itemDesc = ItemsMeta.ItemsDesc[item];
        Debug.Assert(itemDesc.IsWeapon);
        itemDesc.Equip((ItemsMeta.WeaponSlot)slotIdx);
        
        if (notify) OnInventoryLayoutChange();
    }
    
    public BaseItemDesc GetItemDesc(int slotIdx)
    {
        Debug.Assert(slotIdx < Items.Count);
        if (Items[slotIdx] == ItemsMeta.Item.Null)
        {
            return null;
        }
        
        return ItemsMeta.ItemsDesc[Items[slotIdx]];
    }
    
    public ItemsMeta.Item TakeWeapon(int slotIdx, bool notify = true)
    {
        Debug.Assert(slotIdx < Weapons.Count);
        if (Weapons[slotIdx] == ItemsMeta.Item.Null)
        {
            return ItemsMeta.Item.Null;
        }

        ItemsMeta.Item item = Weapons[slotIdx];
        Weapons[slotIdx] = ItemsMeta.Item.Null;
        
        BaseItemDesc itemDesc = ItemsMeta.ItemsDesc[item];
        itemDesc.Unequip();
        
        if (notify) OnInventoryLayoutChange();
        return item;
    }
    
    public void PutItem(ItemsMeta.Item item, int slotIdx, bool notify = true)
    {
        Debug.Assert(slotIdx < Items.Count);
        Debug.Assert(Items[slotIdx] == ItemsMeta.Item.Null);
        Debug.Assert(item != ItemsMeta.Item.Null);
        
        Items[slotIdx] = item;
        if (notify) OnInventoryLayoutChange();
    }
    
    public ItemsMeta.Item TakeItem(int slotIdx, bool notify = true)
    {
        Debug.Assert(slotIdx < Items.Count);
        
        ItemsMeta.Item item = Items[slotIdx];
        Items[slotIdx] = ItemsMeta.Item.Null;
        
        if (notify) OnInventoryLayoutChange();
        return item;
    }

    public void Use(int cellId)
    {
        if (Items[cellId] == ItemsMeta.Item.Null)
        {
            return;
        }
        
        BaseItemDesc itemDesc = ItemsMeta.ItemsDesc[Items[cellId]];
        itemDesc.Use();
        if (itemDesc.OneTimeUsable)
        {
            Items[cellId] = ItemsMeta.Item.Null;
            OnInventoryLayoutChange();
        }
    }
}
