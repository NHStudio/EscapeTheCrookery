using System;
using UnityEngine;

public class ShotgunItemDesc : BaseItemDesc
{
    private static BaseItemDesc _instance;
    public static BaseItemDesc Instance => _instance ??= new ShotgunItemDesc();

    private Shotgun _shotgun;
    private ItemsMeta.WeaponSlot _slot;

    protected ShotgunItemDesc()
    {
        Name = "Shotgun";
        IsWeapon = true;
        Icon = Resources.Load<Sprite>("Sprites/itemB");
        OneTimeUsable = false;
        
        _shotgun = _player.GetComponent<Shotgun>();
    }

    public override void Use()
    {
    }

    public override void Equip(ItemsMeta.WeaponSlot slot)
    {
        // Assuming that there is no equipment in the slot
        _shotgun.enabled = true;
        _slot = slot;
        
        switch (slot)
        {
            case ItemsMeta.WeaponSlot.Main:
                _playerController.MainWeapon = _shotgun;
                break;
            
            case ItemsMeta.WeaponSlot.Secondary:
                _playerController.SecondaryWeapon = _shotgun;
                break;
        
            default:
                throw new ArgumentOutOfRangeException(nameof(slot), slot, null);
        }
    }

    public override void Unequip()
    {
        _shotgun.enabled = true;
        
        switch (_slot)
        {
            case ItemsMeta.WeaponSlot.Main:
                _playerController.MainWeapon = null;
                break;
            
            case ItemsMeta.WeaponSlot.Secondary:
                _playerController.SecondaryWeapon = null;
                break;
        
            default:
                throw new ArgumentOutOfRangeException(nameof(_slot), _slot, null);
        }
    }
}