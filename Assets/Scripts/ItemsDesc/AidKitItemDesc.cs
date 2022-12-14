using UnityEngine;

public class AidKitItemDesc : BaseItemDesc
{
    private static BaseItemDesc _instance;
    public static BaseItemDesc Instance { get; private set; }

    protected AidKitItemDesc()
    {
        Name = "Aid Kit";
        Icon = Resources.Load<Sprite>("Sprites/MedicUp");
        OneTimeUsable = true;
    }

    public static BaseItemDesc RecreateInstance()
    {
        return Instance = new AidKitItemDesc();
    }

    public override void Use()
    {
        _playerController.Parameters.HitPoints += PlayerStatsManager.Instance.stats.medicBaseHeal;
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