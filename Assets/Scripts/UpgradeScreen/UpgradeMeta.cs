using System;
using System.Linq;

// The enum is created in order to make it possible to choose one from the unity editor.
public enum UpgradeType
{
    Damage,
    DropAmount,
    FireRate,
    Health,
    AidKitHeal,
    Luck
}

public class UpgradeMeta
{
    public UpgradeType type;
    public string name;
    public Type upgradeClass;

    public static UpgradeMeta[] upgrades = new UpgradeMeta[]
    {
        new UpgradeMeta { type = UpgradeType.Damage, name = "Damage", upgradeClass = typeof(DmgUP) },
        new UpgradeMeta { type = UpgradeType.DropAmount, name = "Drop Amount", upgradeClass = typeof(DropAmountUP) },
        new UpgradeMeta { type = UpgradeType.FireRate, name = "Fire Rate", upgradeClass = typeof(FireRateUP) },
        new UpgradeMeta { type = UpgradeType.Health, name = "Health", upgradeClass = typeof(HealthUp) },
        new UpgradeMeta { type = UpgradeType.AidKitHeal, name = "Aid Kit Heal", upgradeClass = typeof(MedicUP) },
        new UpgradeMeta { type = UpgradeType.Luck, name = "Luck", upgradeClass = typeof(LuckUP) }
    };

    public static UpgradeMeta GetUpgradeWithType(UpgradeType type)
    {
        return upgrades.FirstOrDefault(t => t.type == type);
    }
}