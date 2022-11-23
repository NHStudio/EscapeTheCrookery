
public class DropAmountUP : UpgradeImpl
{
    public override void ApplyUpgrade(PlayerStats playerStats)
    {
        playerStats.dropAmount += _level;
    }

    public override int GetMaxLevel()
    {
        return 5;
    }
}