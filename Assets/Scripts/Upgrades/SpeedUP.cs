
public class SpeedUP : UpgradeImpl
{
    public override void ApplyUpgrade(PlayerStats stats)
    {
        stats.speed += 0.5f * _level;
    }

    public override int GetMaxLevel()
    {
        return 6;
    }
}