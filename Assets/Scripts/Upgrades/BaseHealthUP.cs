using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseHealthUP : MonoBehaviour, ClickerBase {
    public TMP_Text title;

    public void Start() {
        title.text = string.Format("Player Health: {0}/{1}", GetCurrentLevel(), GetMaxLevel());
    }

    public void Action() {
        tmp.PlayerBaseHealth++;
    }
    public int GetCurrentLevel() {
        return tmp.PlayerBaseHealth;
    }
    public int GetMaxLevel() {
        return 10;
    }

    public void OnMouseDown() {
        if (Wallet.Instance.Count() >= 5 && GetCurrentLevel() < GetMaxLevel()) {
            Action();
            title.text = string.Format("Player Health: {0}/{1}", GetCurrentLevel(), GetMaxLevel());
            Wallet.Instance.Dec(5);
        }
    }
}
