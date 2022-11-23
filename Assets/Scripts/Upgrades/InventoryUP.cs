using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUP : MonoBehaviour, ClickerBase {
    public TMP_Text title;

    public void Start() {
        title.text = string.Format("Inventory Size: {0}/{1}", GetCurrentLevel(), GetMaxLevel());
    }

    public void Action() {
        Inventory.Instance.Extend();
    }
    public int GetCurrentLevel() {
        return Inventory.Instance.Items.Count;
    }
    public int GetMaxLevel() {
        return 5;
    }

    public void OnMouseDown() {
        if (Wallet.Instance.Count() >= 5 && GetCurrentLevel() < GetMaxLevel()) {
            Action();
            title.text = string.Format("Inventory Size: {0}/{1}", GetCurrentLevel(), GetMaxLevel());
            Wallet.Instance.Dec(5);
        }
    }
}
