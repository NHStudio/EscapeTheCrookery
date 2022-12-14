
public class DmgUP : UpgradeBase
{
    public override void ApplyUpgrade(PlayerStats playerStats)
    {
        playerStats.basePlayerDamage += level;
    }

    public override int GetMaxLevel()
    {
        return 5;
    }

    public override int GetUpgradeCost()
    {
        return 5 + level * level;
    }
}