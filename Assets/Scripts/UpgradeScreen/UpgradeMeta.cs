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
    public Func<UpgradeBase> factory;

    public static UpgradeMeta[] upgrades = {
        new() { type = UpgradeType.Damage, name = "Damage", factory = () => new DmgUP() },
        new() { type = UpgradeType.DropAmount, name = "Drop Amount", factory = () => new DropAmountUP() },
        new() { type = UpgradeType.FireRate, name = "Fire Rate", factory = () => new FireRateUP() },
        new() { type = UpgradeType.Health, name = "Health", factory = () => new HealthUp() },
        new() { type = UpgradeType.AidKitHeal, name = "Aid Kit Heal", factory = () => new MedicUP() },
        new() { type = UpgradeType.Luck, name = "Luck", factory = () => new LuckUP() }
    };

    public static UpgradeMeta GetUpgradeMetaWithType(UpgradeType type)
    {
        return upgrades.FirstOrDefault(t => t.type == type);
    }
}