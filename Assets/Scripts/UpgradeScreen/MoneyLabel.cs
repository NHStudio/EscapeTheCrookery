using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyLabel : MonoBehaviour
{
    // Listens to Wallet events and update TextMesh text accordingly to the current wallet balance.
    // This is a simple example of how to use Wallet events.
    
    private TMP_Text textMesh;
    
    private void Start()
    {
        textMesh = GetComponent<TMP_Text>();
        Wallet.Instance.OnMoneyChanged += OnMoneyChanged;
        OnMoneyChanged(Wallet.Instance.Count());
    }
    
    private void OnMoneyChanged(int money)
    {
        textMesh.text = "Money: " + money;
    }
    
    private void OnDestroy()
    {
        Wallet.Instance.OnMoneyChanged -= OnMoneyChanged;
    }
}
