
public class LuckUP : UpgradeImpl
{
    public override void ApplyUpgrade(PlayerStats playerStats)
    {
        playerStats.luck += 0.05f * _level;
    }

    public override int GetMaxLevel()
    {
        return 50;
    }
}