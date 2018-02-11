using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPanel : MonoBehaviour
{
    public Text dialog;

    // Use this for initialization
    void Start()
    {
        dialog.text = "Hi";
    }

    public void UpdateDialog(string msgToLog)
    {
        dialog.text = msgToLog;
    }
}
