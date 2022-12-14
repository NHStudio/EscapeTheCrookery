using System;
using UnityEngine;

public class GunItemDesc : BaseItemDesc
{
    private static BaseItemDesc _instance;
    public static BaseItemDesc Instance { get; private set; }

    private BaseShootingWeapon _gun;
    private ItemsMeta.WeaponSlot _slot;

    protected GunItemDesc()
    {
        Name = "Gun";
        IsWeapon = true;
        Icon = Resources.Load<Sprite>("Sprites/itemA");
        OneTimeUsable = false;
    }
    
    public static BaseItemDesc RecreateInstance()
    {
        return Instance = new GunItemDesc();
    }

    public override void Use()
    {
    }

    public override void Equip(ItemsMeta.WeaponSlot slot)
    {
        _gun = _player.GetComponent<Gun>();
        // Assuming that there is no equipment in the slot
        _gun.enabled = true;
        _slot = slot;
        
        switch (slot)
        {
        case ItemsMeta.WeaponSlot.Main:
            _playerController.MainWeapon = _gun;
            break;
            
        case ItemsMeta.WeaponSlot.Secondary:
            _playerController.SecondaryWeapon = _gun;
            break;
        
        default:
            throw new ArgumentOutOfRangeException(nameof(slot), slot, null);
        }
    }

    public override void Unequip()
    {
        _gun.enabled = false;

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