using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
            OnClickStart();
	}

    public void OnClickStart()
    {
        SceneManager.LoadScene("OverworldScene");
    }

    public void OnClickInstructions()
    {
        transform.Find("Panel").gameObject.SetActive(true);
        GameObject.Find("OpenSound").GetComponent<AudioSource>().Play();
    }

    public void OnClickInstructionsClose()
    {
        transform.Find("Panel").gameObject.SetActive(false);
        GameObject.Find("CloseSound").GetComponent<AudioSource>().Play();
    }

}
