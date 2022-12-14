using UnityEngine;

public class InvunerabilityItemDesc : BaseItemDesc
{
    private static BaseItemDesc _instance;
    public static BaseItemDesc Instance => _instance ??= new InvunerabilityItemDesc();

    protected InvunerabilityItemDesc()
    {
        Name = "Temporary Invunerability";
        Icon = Resources.Load<Sprite>("Sprites/Invunerability");
        OneTimeUsable = true;
    }

    public override void Use()
    {
        _playerController.Parameters.ApplyInvunerability();
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