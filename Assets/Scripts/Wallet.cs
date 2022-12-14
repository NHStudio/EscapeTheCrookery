using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet {
    public static Wallet Instance = new();
    private int _money = 5;
    public event Action<int> OnMoneyChanged;

    public int Count() {
        return _money;
    }
    
    public void Add(int cnt)
    {
        _money += cnt;
        if(OnMoneyChanged != null)
            OnMoneyChanged(_money);
    }

    public void Dec(int cnt) {
        _money -= cnt;
        if(OnMoneyChanged != null)
            OnMoneyChanged(_money);
    }
}
