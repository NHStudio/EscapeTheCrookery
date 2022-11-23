using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FireRateUP : MonoBehaviour, ClickerBase {
    public TMP_Text title;

    public void Start() {
        title.text = string.Format("Fire Rate: {0}/{1}", GetCurrentLevel(), GetMaxLevel());
    }

    public void Action() {
        tmp.FireRate += 0.5f;
    }
    public int GetCurrentLevel() {
        return (int)(tmp.FireRate * 2);
    }
    public int GetMaxLevel() {
        return 6;
    }

    public void OnMouseDown() {
        if (Wallet.Instance.Count() >= 5 && GetCurrentLevel() < GetMaxLevel()) {
            Action();
            title.text = string.Format("Fire Rate: {0}/{1}", GetCurrentLevel(), GetMaxLevel());
            Wallet.Instance.Dec(5);
        }
    }
}
