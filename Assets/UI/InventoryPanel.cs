using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public Text melatoninCount;
    // Use this for initialization
    void Start()
    {
        melatoninCount.text = GameData.Instance.CurrentInventory.GetCountOf("melatonin").ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdatePanelCount()
    {
        melatoninCount.text = GameData.Instance.CurrentInventory.GetCountOf("melatonin").ToString();
    }
}
