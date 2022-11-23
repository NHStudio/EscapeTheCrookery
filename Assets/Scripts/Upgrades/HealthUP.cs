
public class HealthUp : UpgradeImpl
{
    public override void ApplyUpgrade(PlayerStats stats)
    {
        stats.playerBaseHealth += _level;
    }

    public override int GetMaxLevel()
    {
        return 10;
    }
}