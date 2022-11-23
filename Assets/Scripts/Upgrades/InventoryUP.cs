
public class InventoryUP : UpgradeBase
{
    public override void ApplyUpgrade(PlayerStats playerStats)
    {
        playerStats.inventorySize += _level;
    }

    public override int GetMaxLevel()
    {
        return 5;
    }
    
    public override int GetUpgradeCost()
    {
        return 5 + _level * _level;
    }
}