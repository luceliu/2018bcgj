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

    public int GetCountOf(string item)
    {
        try
        {
            return itemDict[item];
        }
        catch
        {
            Debug.Log("None of this item.");
            return 0;
        }
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
            GameData.Instance.inventoryPanel.UpdatePanelCount();
            Destroy(item.gameObject);
            Debug.Log("Destroyed");
        }
    }

    public override void GetItemsInInventory()
    {
        Debug.Log("Inventory:");
        foreach (KeyValuePair<string, int> entry in itemDict)
        {
            // do something with entry.Value or entry.Key
            Debug.Log("<" + entry.Key + ", " + entry.Value + ">");
        }
    }

    public override void Use(string item)
    {
        if (itemDict.ContainsKey(item) && itemDict[item] > 0)
        {
            UseAccordingly(item);
        }

        else
        {
            Debug.Log("You don't have this item! Can't use.");
        }
    }

    public void UseAccordingly(string item)
    {
        switch (item)
        {
            case "melatonin":
                if (!GameData.Instance.TookMelatonin)
                {
                    itemDict[item] -= 1;
                    Debug.Log("You used " + item + "!");
                    GameData.Instance.inventoryPanel.UpdatePanelCount();
                    GameData.Instance.TookMelatonin = true;
                    Debug.Log("GameData knows that you took melatonin");
                }
                else
                {
                    Debug.Log("You've already taken melatonin today!");
                }
                break;
        }
    }

}
