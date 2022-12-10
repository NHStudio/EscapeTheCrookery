
public class SpeedUP : UpgradeBase
{
    public override void ApplyUpgrade(PlayerStats stats)
    {
        stats.speed += 0.5f * level;
    }

    public override int GetMaxLevel()
    {
        return 6;
    }
    
    public override int GetUpgradeCost()
    {
        return 5 + level * level;
    }
}