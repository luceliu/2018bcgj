using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DialogPanel : MonoBehaviour
{
    public List<string> msgs;
    public Text dialog;
    private int noMsgsToDisplay = 5;

    // Use this for initialization
    void Start()
    {
        msgs = new List<string>();
        dialog.text = "";
    }

    public void UpdateDialog(string msgToLog)
    {
        int count = msgs.Count;
        string newDialog = "";
        string time = GetTime();
        msgs.Add(time + msgToLog + "\n");
        IEnumerable<string> displayMsgs = msgs.Skip(count - noMsgsToDisplay);

        foreach (string msg in displayMsgs)
        {
            newDialog += msg;
        }

        dialog.text = newDialog;
    }

    private static string GetTime()
    {
        string time = "" + System.DateTime.Now + "] ";
        int start = (time.IndexOf(":", System.StringComparison.CurrentCulture)) - 2;
        time = "[" + time.Substring(start);
        return time;
    }
}
