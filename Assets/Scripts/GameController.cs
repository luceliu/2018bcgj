using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController ActualInstance;
    public GameObject finishFlag;
    public Enemy targetEnemy;

    void Awake()
    {
        VideoModeManager.SetDreamworld();
        finishFlag.gameObject.SetActive(false);
        targetEnemy = GameObject.Find("Enemy").GetComponent<Enemy>();
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

    public void DefeatedTargetEnemy() // something else will call this 
    {
        // Set finish flag to be visible
        finishFlag.gameObject.SetActive(true);
    }

    public void ReachFlagAndEndDream() // insert some victorious transition back to overworld
    {

    }
}
