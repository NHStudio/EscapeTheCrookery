using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    public List<ItemsMeta.Item> Weapons;
    public List<ItemsMeta.Item> Items;

    public static InventoryManager Instance { get; private set; }

    public event Action OnInventoryLayoutChange;

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
        if (Input.GetKeyDown(KeyCode.Alpha1)) Use(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Use(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Use(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) Use(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) Use(4);
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
                    Weapons[slotIdx] = item;
                    itemDesc.Equip(slot);
                    Debug.Log($"Equipped {item} to slot {slotIdx}");
                    OnInventoryLayoutChange();
                    return true;
                }
            }
        }
        
        for (int i = 0; i < Items.Count; ++i) {
            if (Items[i] == ItemsMeta.Item.Null) {
                Items[i] = item;
                OnInventoryLayoutChange();
                return true;
            }
        }

        return false;
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
