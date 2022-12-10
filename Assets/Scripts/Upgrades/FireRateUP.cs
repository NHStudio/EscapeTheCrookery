
public class FireRateUP : UpgradeBase
{
    public override void ApplyUpgrade(PlayerStats playerStats)
    {
        playerStats.fireRate += 0.5f * level;
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