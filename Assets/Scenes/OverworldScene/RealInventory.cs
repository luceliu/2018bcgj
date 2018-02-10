using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealInventory : Inventory
{
    public int maxNoItems = 4; // can be changed later thru powerups?

    public RealInventory()
    {
        itemDict = new Dictionary<Item, int>();
        GameData.Instance.ChangeSceneBecauseREasons();
    }

    public bool CanPickUpMore()
    {
        if (itemDict.Count < maxNoItems)
        {
            return true;
        }

        return false;
    }
}
