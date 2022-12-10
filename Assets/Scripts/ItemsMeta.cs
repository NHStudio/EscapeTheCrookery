using System.Collections.Generic;

public class ItemsMeta
{
    public enum Item
    {
        Null = 0,
        Gun,
        Shotgun,
        AidKit
    };

    public enum WeaponSlot
    {
        Main = 0,
        Secondary = 1
    }

    // To be filled by InventoryManager
    public static Dictionary<Item, BaseItemDesc> ItemsDesc;
}