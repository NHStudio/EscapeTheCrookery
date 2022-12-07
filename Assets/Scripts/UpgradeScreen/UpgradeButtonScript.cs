using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeButtonScript : MonoBehaviour
{
    public UpgradeType upgradeType = UpgradeType.Damage;
    private UpgradeMeta upgrade = null;

    public TMP_Text costText;
    public TMP_Text titleText;
    
    private void Awake()
    {
        upgrade = UpgradeMeta.GetUpgradeMetaWithType(upgradeType);
        Wallet.Instance.OnMoneyChanged += OnMoneyChanged;
        UpdateAvailability();
        UpdateTitle();
    }
    
    private void OnDestroy()
    {
        Wallet.Instance.OnMoneyChanged -= OnMoneyChanged;
    }

    private void UpdateTitle()
    {
        var playerUpgrade = PlayerStatsManager.Instance.GetUpgrade(upgrade);
        if (playerUpgrade == null)
        {
            titleText.text = upgrade.name;
        }
        else
        {
            titleText.text = $"{upgrade.name} ({playerUpgrade.GetCurrentLevel()} / {playerUpgrade.GetMaxLevel()})";
        }
    }

    private void UpdateAvailability()
    {
        var cost = PlayerStatsManager.Instance.GetUpgradeCost(upgrade);
        var currentMoney = Wallet.Instance.Count();

        if (PlayerStatsManager.Instance.IsMaxedOut(upgrade))
        {
            costText.color = Color.green;
            costText.text = "Maxed Out";
        }
        else if (cost == int.MaxValue)
        {
            costText.color = Color.red;
            costText.text = "Not Available";
        }
        else
        {
            costText.color = cost > currentMoney ? Color.red : Color.yellow;
            costText.text = "Cost: " + cost;
        }
    }

    private void OnMoneyChanged(int money)
    {
        UpdateAvailability();
    }

    public void OnMouseDown()
    {
        var cost = PlayerStatsManager.Instance.GetUpgradeCost(upgrade);
        // Withdraw the cost from the player's balance
        if (!(Wallet.Instance.Count() >= cost)) return;
        if (!PlayerStatsManager.Instance.AddUpgrade(upgrade)) return;
        
        Wallet.Instance.Dec(cost);
        UpdateTitle();
    }
}