
public class MedicUP : UpgradeBase
{
    public override void ApplyUpgrade(PlayerStats stats)
    {
        stats.medicBaseHeal += level;
    }

    public override int GetMaxLevel()
    {
        return 7;
    }
    
    public override int GetUpgradeCost()
    {
        return 5 + level * level;
    }
}