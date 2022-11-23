
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    public static readonly Inventory Instance = new();
    public List<GameObject> Weapons = new(2);
    public List<GameObject> Items = new(1);
    
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
