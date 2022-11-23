using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgUP : MonoBehaviour, ClickerBase {
    public TMP_Text title;

    public void Start() {
        title.text = string.Format("Damage: {0}/{1}", GetCurrentLevel(), GetMaxLevel());
        // PlayerParameters.Instance.attackDamage = 1;
    }

    public void Action() {
        tmp.BaseDamage++; // Local Tests
        // PlayerParameters.Instance.attackDamage++;
    }
    public int GetCurrentLevel() {
        return tmp.BaseDamage; // Local Tests
        // return PlayerParameters.Instance.attackDamage;
    }
    public int GetMaxLevel() {
        return 5;
    }

    public void OnMouseDown() {
        if (Wallet.Instance.Count() >= 5 && GetCurrentLevel() < GetMaxLevel()) {
            Action();
            title.text = string.Format("Damage: {0}/{1}", GetCurrentLevel(), GetMaxLevel());
            Wallet.Instance.Dec(5);
        }
    }
}
