using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseHealthUP : MonoBehaviour, ClickerBase {
    public TMP_Text title;

    public void Start() {
        title.text = string.Format("Player Health: {0}/{1}", GetCurrentLevel(), GetMaxLevel());
        // PlayerParameters.Instance.hitPoints = 3;
    }

    public void Action() {
        tmp.PlayerBaseHealth++; // Local Tests
        // PlayerParameters.Instance.hitPoints++;
    }
    public int GetCurrentLevel() {
        return tmp.PlayerBaseHealth; // Local Tests
        // return PlayerParameters.Instance.hitPoints;
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
