using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{

    public bool hasDied;

	// Use this for initialization
	void Start ()
	{
	    hasDied = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if (transform.position.y < -8)
	    {
	        PlayerDies();
	    }
	}

    public void PlayerDies()
    {
        SceneManager.LoadScene("DreamScene");
        
        // Doesn't have to reset scene. Can do take damage -> invuln
    }
}
