
public class InventoryUP : UpgradeImpl
{
    public override void ApplyUpgrade(PlayerStats playerStats)
    {
        playerStats.inventorySize += _level;
    }

    public override int GetMaxLevel()
    {
        return 5;
    }
}