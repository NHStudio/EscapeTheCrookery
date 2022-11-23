using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MedicUP : MonoBehaviour, ClickerBase {
    public TMP_Text title;

    public void Start() {
        title.text = string.Format("Aid Kit heal: {0}/{1}", GetCurrentLevel(), GetMaxLevel());
    }

    public void Action() {
        tmp.MedicBaseHeal++; // Что-то типа static global MedicBase
    }
    public int GetCurrentLevel() {
        return tmp.MedicBaseHeal;
    }
    public int GetMaxLevel() {
        return 7;
    }

    public void OnMouseDown() {
        if (Wallet.Instance.Count() >= 5 && GetCurrentLevel() < GetMaxLevel()) {
            Action();
            title.text = string.Format("Aid Kit heal: {0}/{1}", GetCurrentLevel(), GetMaxLevel());
            Wallet.Instance.Dec(5);
        }
    }
}
