using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item : MonoBehaviour
{

    public string itemID;
    public bool inInventory;

    void Start()
    {
        inInventory = false;
    }

    void Update()
    {
        if (inInventory)
        {
            GetComponent<Renderer>().enabled = false;
            Debug.Log("should have disabled");
        }
    }

}
