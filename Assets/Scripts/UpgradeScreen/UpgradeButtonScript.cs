using TMPro;
using UnityEngine;

public class UpgradeButtonScript : MonoBehaviour
{
    public float cost = 0.0f;
    public UpgradeType upgradeType = UpgradeType.Damage;
    private UpgradeMeta upgrade = null;

    public TMP_Text titleText;
    
    private void Awake()
    {
        upgrade = UpgradeMeta.GetUpgradeWithType(upgradeType);
        titleText.text = upgrade.name;
    }

    public void OnMouseDown() {
        
    }
}