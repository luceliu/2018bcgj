using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamGameController : MonoBehaviour
{
    public static DreamGameController Instance = null;

    private int _tiredLevel;
    private int _maxTiredLevel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start ()
	{
	    ResetTiredLevel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerDreamDeath()
    {
        _tiredLevel++;
        
        // Call back to Real World game controller
        if (_tiredLevel >= _maxTiredLevel)
        {
            // do something
        }
    }

    public void PlayerDreamSuccess()
    {
        ResetTiredLevel();

        // Call back to Real World game controller
    }


    private void ResetTiredLevel()
    {
        _tiredLevel = 0;
    }
}
