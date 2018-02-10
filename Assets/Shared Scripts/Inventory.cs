using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : ScriptableObject
{

    public Dictionary<Item, int> itemDict;

}
