using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleTestController : MonoBehaviour
{
    public Text LastSceneText;
    public Text ResolutionText;
    public Text LastZoneText;

    void Awake()
    {
        //VideoModeManager.Init();
        VideoModeManager.SetDreamworld();

        ResolutionText.text = (VideoModeManager.GetNicelyFormattedString());
    }

    // Use this for initialization
    void Start ()
    {
        var lastScene = GameData.Instance.LastScene;
        LastSceneText.text = string.Format("Last Scene: {0} {1}", lastScene.ToString(), lastScene == SceneType.OverworldScene ? "√" : "X");
        LastZoneText.text = string.Format("Last Zone: {0}", GameData.Instance.LastZone);

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnClickWin()
    {
        GameData.Instance.BattleResult = true;
        SceneManager.LoadScene(GameData.OverworldSceneName);
    }

    public void OnClickLose()
    {
        GameData.Instance.BattleResult = false;
        SceneManager.LoadScene(GameData.OverworldSceneName);
    }
}
