
public class MedicUP : UpgradeImpl
{
    public override void ApplyUpgrade(PlayerStats stats)
    {
        stats.medicBaseHeal += _level;
    }

    public override int GetMaxLevel()
    {
        return 7;
    }
}