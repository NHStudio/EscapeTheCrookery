
public class FireRateUP : UpgradeImpl
{
    public override void ApplyUpgrade(PlayerStats playerStats)
    {
        playerStats.fireRate += 0.5f * _level;
    }

    public override int GetMaxLevel()
    {
        return 6;
    }
}