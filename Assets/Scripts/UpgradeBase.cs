using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats
{
    public int playerBaseHealth = 3;
    public int medicBaseHeal = 3;
    public int baseDamage = 1;
    public float speed = 1;
    public float fireRate = 1;
    public float luck = 0.1f;
    public int dropAmount = 1;
    public int inventorySize = 1;
}

public class PlayerStatsManager
{
    public static PlayerStatsManager Instance = new();
    private PlayerStats stats = new();
    private Dictionary<Type, UpgradeImpl> upgrades;
    
    private bool AddUpgrade<T>() where T : UpgradeImpl, new()
    {
        if (!upgrades.ContainsKey(typeof(T))) {
            upgrades.Add(typeof(T), new T());
        }
        
        var upgrade = upgrades[typeof(T)];
        if (upgrade.GetCurrentLevel() >= upgrade.GetMaxLevel()) return false;
        upgrade.AddLevel();
        return true;
    }
}

public abstract class UpgradeImpl
{
    protected int _level = 0;

    public int GetCurrentLevel()
    {
        return _level;
    }

    public void AddLevel()
    {
        _level++;
    }
    
    public abstract void ApplyUpgrade(PlayerStats stats);
    public abstract int GetMaxLevel();
}