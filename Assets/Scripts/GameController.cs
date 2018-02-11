using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController ActualInstance;
    public GameObject finishFlag;
    public MathMonsterController targetEnemy;
    public bool flagActiveAtStart;

    void Awake()
    {
        VideoModeManager.SetDreamworld();
        finishFlag = GameObject.Find("FinishFlag");
        finishFlag.gameObject.SetActive(flagActiveAtStart);
        //if (!flagActiveAtStart)
        //{
        //    //targetEnemy = GameObject.Find("Enemy").GetComponent<MathMonsterController>();
        //}
    }

    // Use this for initialization
    void Start()
    {
        SoundManager.Instance.StartMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetEnemy == null)
        {
            DefeatedTargetEnemy();
        }
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
        GameObject.Find("GameController").GetComponent<GameController>().WakeUp(true);
    }
}
