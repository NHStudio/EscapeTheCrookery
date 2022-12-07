
public class HealthUp : UpgradeBase
{
    public override void ApplyUpgrade(PlayerStats stats)
    {
        stats.playerBaseHealth += _level;
    }

    public override int GetMaxLevel()
    {
        return 10;
    }
    
    public override int GetUpgradeCost()
    {
        return 5 + _level * _level;
    }
}