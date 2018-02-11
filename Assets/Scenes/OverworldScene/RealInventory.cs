using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealInventory : Inventory
{
    new public int maxNoItems = 4; // can be changed later thru powerups?

    new public Dictionary<Item, int> itemDict = new Dictionary<Item, int>();

    public override bool IsThereSpace()
    {
        if (itemDict.Count < maxNoItems)
        {
            return true;
        }

        return false;
    }

    public override void AddItem(Item item)
    {
        if (IsThereSpace() && IsNotAlreadyInInventory(item))
        {
            if (itemDict.ContainsKey(item))
            {
                itemDict[item] += 1;
                Debug.Log("Picked up another " + item.itemID);
            }

            else
            {
                itemDict.Add(item, 1); ;
                Debug.Log("Picked up a brand new " + item.itemID);
            }

            item.inInventory = true;
            Destroy(item.gameObject);
            Debug.Log("Destroyed");
        }
    }

    private bool IsNotAlreadyInInventory(Item item)
    {
        if (!item.inInventory) { return true; }
        return false;
    }

    public override void GetItemsInInventory()
    {
        foreach (KeyValuePair<Item, int> entry in itemDict)
        {
            // do something with entry.Value or entry.Key
            Debug.Log("<" + entry.Key + ", " + entry.Value + ">");
        }
    }
}
