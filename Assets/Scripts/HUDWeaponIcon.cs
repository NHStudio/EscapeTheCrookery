using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDWeaponIcon : MonoBehaviour
{
    public ItemsMeta.WeaponSlot weaponSlot = ItemsMeta.WeaponSlot.Main;
    
    private Image _image;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        InventoryManager.Instance.OnInventoryLayoutChange += OnInventoryLayoutChange;
    }

    private void OnInventoryLayoutChange()
    {
        ItemsMeta.Item weapon = InventoryManager.Instance.Weapons[(int)weaponSlot];
        if (weapon != ItemsMeta.Item.Null)
        {
            _image.sprite = ItemsMeta.ItemsDesc[weapon].Icon;
        }
        else
        {
            _image.sprite = null;
        }
    }
}
