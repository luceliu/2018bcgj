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

    }

    void Update()
    {
        if (inInventory)
        {
            this.GetComponent<Renderer>().enabled = false;
        }
    }

}
