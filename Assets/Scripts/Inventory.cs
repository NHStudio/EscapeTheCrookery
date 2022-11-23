using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{

    public static Inventory Instance;
    public List<GameObject> Weapons = new List<GameObject>(2);
    public List<GameObject> Items = new List<GameObject>(1);

    private void Awake() {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Extend() {
        Items.Add(null);
    }
}
