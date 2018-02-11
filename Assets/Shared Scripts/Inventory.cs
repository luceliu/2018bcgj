using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Inventory : ScriptableObject
{
    public int maxNoItems;
    public Dictionary<string, int> itemDict;

    public abstract bool IsThereSpace();

    public abstract void AddItem(Item item);

    public abstract void GetItemsInInventory();
}
