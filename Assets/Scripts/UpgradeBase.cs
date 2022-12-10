using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public abstract class UpgradeBase
{
    protected int level = 0;

    public int GetCurrentLevel()
    {
        return level;
    }

    public void AddLevel()
    {
        level++;
    }

    public abstract void ApplyUpgrade(PlayerStats stats);
    public abstract int GetMaxLevel();
    public abstract int GetUpgradeCost();
}