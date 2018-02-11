using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealInventory : Inventory
{
    new public int maxNoItems = 4; // can be changed later thru powerups?

    new public Dictionary<string, int> itemDict = new Dictionary<string, int>();

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
        if (IsThereSpace())
        {
            if (itemDict.ContainsKey(item.itemID))
            {
                itemDict[item.itemID] += 1;
                Debug.Log("Picked up another " + item.itemID);
            }

            else
            {
                itemDict.Add(item.itemID, 1);
                Debug.Log("Picked up a brand new " + item.itemID);
            }

            //item.inInventory = true;
            Destroy(item.gameObject);
            Debug.Log("Destroyed");
        }
    }

    public override void GetItemsInInventory()
    {
        foreach (KeyValuePair<string, int> entry in itemDict)
        {
            // do something with entry.Value or entry.Key
            Debug.Log("<" + entry.Key + ", " + entry.Value + ">");
        }
    }
}
