using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    void Awake()
    {
        VideoModeManager.SetDreamworld();
    }

    // Use this for initialization
    void Start()
    {
        SoundManager.Instance.StartMusic();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WakeUp(bool battleWon)
    {
        GameData.Instance.BattleResult = battleWon;
        GameData.Instance.TookMelatonin = false;
        SceneManager.LoadScene(GameData.OverworldSceneName);
    }
}
