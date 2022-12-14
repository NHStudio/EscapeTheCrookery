using UnityEngine;

public class AttackIncreaseItemDesc : BaseItemDesc
{
    private static BaseItemDesc _instance;
    public static BaseItemDesc Instance { get; private set; }

    protected AttackIncreaseItemDesc()
    {
        Name = "Temporary Attack Increase";
        Icon = Resources.Load<Sprite>("Sprites/Attack Increase");
        OneTimeUsable = true;
    }
    
    public static BaseItemDesc RecreateInstance()
    {
        return Instance = new AttackIncreaseItemDesc();
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