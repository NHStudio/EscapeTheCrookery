using UnityEngine;

public class AttackIncreaseItemDesc : BaseItemDesc
{
    private static BaseItemDesc _instance;
    public static BaseItemDesc Instance => _instance ??= new AttackIncreaseItemDesc();

    protected AttackIncreaseItemDesc()
    {
        Name = "Temporary Attack Increase";
        Icon = Resources.Load<Sprite>("Sprites/Invunerability");
        OneTimeUsable = true;
    }

    public override void Use()
    {
        _playerController.Parameters.ApplyAttackMultiplier();
    }

    public override void Equip(ItemsMeta.WeaponSlot slot)
    {
        throw new System.NotImplementedException();
    }
    
    public override void Unequip()
    {
        throw new System.NotImplementedException();
    }
}