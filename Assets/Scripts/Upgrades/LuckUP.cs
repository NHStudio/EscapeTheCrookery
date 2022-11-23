using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LuckUP : MonoBehaviour, ClickerBase {
    public TMP_Text title;

    public void Start() {
        title.text = string.Format("Luck: {0}/{1}%", GetCurrentLevel(), GetMaxLevel());
    }

    public void Action() {
        tmp.Luck += 0.05f;
    }
    public int GetCurrentLevel() {
        return (int)(tmp.Luck * 100);
    }
    public int GetMaxLevel() {
        return 50;
    }

    public void OnMouseDown() {
        if (Wallet.Instance.Count() >= 5 && GetCurrentLevel() < GetMaxLevel()) {
            Action();
            title.text = string.Format("Luck: {0}/{1}", GetCurrentLevel(), GetMaxLevel());
            Wallet.Instance.Dec(5);
        }
    }
}
