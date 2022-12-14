
public class LuckUP : UpgradeBase
{
    public override void ApplyUpgrade(PlayerStats playerStats)
    {
        playerStats.luck += 0.05f * level;
    }

    public override int GetMaxLevel()
    {
        return 50;
    }
    
    public override int GetUpgradeCost()
    {
        return 5 + level * level;
    }
}