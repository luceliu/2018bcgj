using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneController : MonoBehaviour
{

    public static bool IgnoreVMMHack;

    // Use this for initialization
    void Start()
    {
        if(!IgnoreVMMHack)
            VideoModeManager.SetMaximum();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
