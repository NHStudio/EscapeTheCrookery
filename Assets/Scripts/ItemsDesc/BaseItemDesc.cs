using UnityEngine;

public abstract class BaseItemDesc
{
    public string Name { get; protected set; }
    public Sprite Icon { get; protected set; }

    public bool IsWeapon { get; protected set; }
    public bool OneTimeUsable { get; protected set; }
    
    protected GameObject _player;
    protected PlayerController _playerController;
    
    protected BaseItemDesc()
    {
        _player = GameObject.FindWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();
    }

    public abstract void Use();
    public abstract void Equip(ItemsMeta.WeaponSlot slot);
    public abstract void Unequip();
}