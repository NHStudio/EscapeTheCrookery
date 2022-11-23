using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour{
    public TMP_Text title;

    public static Wallet Instance;
    private int money = 40/*0*/; // для тестов

    private void Awake() {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        Draw();
    }

    private void Draw() {
        title.text = string.Format("Money: {0}", Count());
    }

    public int Count() {
        return money;
    }
    
    public void Add(int cnt) {
        money += cnt;
        Draw();
    }

    public void Dec(int cnt) {
        money -= cnt;
        Draw();
    }
}
