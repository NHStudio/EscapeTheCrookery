using System.Collections.Generic;

public class PlayerStats
{
    public int playerBaseHealth = 3;
    public int enemyBaseHealth = 3;
    public int medicBaseHeal = 3;
    public int basePlayerDamage = 1;
    public int baseEnemyDamage = 1;
    public float speed = 1;
    public float fireRate = 1;
    public float luck = 0.1f;
    public int dropAmount = 1;
    public int inventorySize = 1;
}

public class PlayerStatsManager
{
    public static PlayerStatsManager Instance = new();
    public PlayerStats stats = new();
    
    private Dictionary<UpgradeMeta, UpgradeBase> upgrades = new();

    public PlayerStatsManager()
    {
        foreach(var upgrade in UpgradeMeta.upgrades)
        {
            upgrades.Add(upgrade, upgrade.factory());
        }
    }

    public bool IsMaxedOut(UpgradeMeta type)
    {
        if (!upgrades.ContainsKey(type))
        {
            return false;
        }

        var upgrade = upgrades[type];
        return upgrade.GetCurrentLevel() >= upgrade.GetMaxLevel();
    }

    public bool AddUpgrade(UpgradeMeta type)
    {
        var upgrade = upgrades[type];
        if (upgrade.GetCurrentLevel() >= upgrade.GetMaxLevel()) return false;
        upgrade.AddLevel();
        UpdateStats();
        return true;
    }

    public void UpdateStats()
    {
        var newStats = new PlayerStats();
        
        foreach (var upgrade in upgrades.Values)
        {
            upgrade.ApplyUpgrade(newStats);
        }
        
        stats = newStats;
    }

    public UpgradeBase GetUpgrade(UpgradeMeta type)
    {
        return upgrades.ContainsKey(type) ? upgrades[type] : null;
    }

    public int GetUpgradeCost(UpgradeMeta upgradeType)
    {
        if (IsMaxedOut(upgradeType))
        {
            return int.MaxValue;
        }
        
        var upgrade = GetUpgrade(upgradeType);
        return upgrade?.GetUpgradeCost() ?? int.MaxValue;
    }
}