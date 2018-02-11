using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsController : MonoBehaviour
{
    public static HeartsController instance = null;
    
    public GameObject Heart3;
    public GameObject Heart2;
    public GameObject Heart1;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

	// Use this for initialization
	void Start ()
	{
	    Heart3.SetActive(true);
	    Heart2.SetActive(true);
	    Heart1.SetActive(true);
    }

    public void UpdateHearts(int heartsToShow)
    {
        if (heartsToShow == 0)
        {
            Heart3.SetActive(false);
            Heart2.SetActive(false);
            Heart1.SetActive(false);
        }
        if (heartsToShow == 1)
        {
            Heart3.SetActive(false);
            Heart2.SetActive(false);
            Heart1.SetActive(true);
        }
        else if (heartsToShow == 2)
        {
            Heart3.SetActive(false);
            Heart2.SetActive(true);
            Heart1.SetActive(true);
        }
        else if (heartsToShow == 3)
        {
            Heart3.SetActive(false);
            Heart2.SetActive(true);
            Heart1.SetActive(true);
        }
    }
}
