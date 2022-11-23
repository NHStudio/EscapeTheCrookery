
public class DmgUP : UpgradeImpl
{
    public override void ApplyUpgrade(PlayerStats playerStats)
    {
        playerStats.baseDamage += _level;
    }

    public override int GetMaxLevel()
    {
        return 5;
    }
}