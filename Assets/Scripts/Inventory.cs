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

    public void Store(GameObject obj) {
        for (int i = 0; i < Items.Count; ++i) {
            if (Items[i] == null) {
                Items[i] = obj;
            }
        }
    }

    public void Activate(int cellId) {
        if (Items[cellId] != null) {
            // Items[cellId].Activate();
            Drop(cellId);
        }
    }

    public void Drop(int cellId) {
        Items[cellId] = null;
    }
}
