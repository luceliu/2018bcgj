using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    static SoundManager Instance
    {
        get
        {

            var smo = GameObject.Find("SoundManager");
            if (smo == null)
            {
                smo = Instantiate<GameObject>(Resources.Load<GameObject>("SoundManager"));
            }

            return smo.GetComponent<SoundManager>();

        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
