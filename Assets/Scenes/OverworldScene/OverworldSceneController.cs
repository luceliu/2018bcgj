using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Overworld
{

    public class OverworldSceneController : MonoBehaviour
    {
        const long EndTicks = 6000000000; //10000000 * 60sec * 10min
        private static long? StartTime;

        public ZoneController ZonePlayerIsIn; //this should probably be encapsulated but Unity hates proper ooop

        void Awake()
        {
            VideoModeManager.Init();
            VideoModeManager.SetOverworld();

            if(!StartTime.HasValue)
            {
                StartTime = System.DateTime.Now.Ticks;
            }
            
        }

        // Use this for initialization
        void Start()
        {
            if (GameData.Instance.BattleResult.HasValue)
                ResolveBattleResult();

            //reset fields
            GameData.Instance.BattleResult = null;
            GameData.Instance.LastScene = SceneType.OverworldScene;
            //GameData.Instance.LastZone = ZoneEnvironment.EmptyRoom;

            //position player
            if(GameData.Instance.PlayerPosition.HasValue)
            {
                GameObject.FindGameObjectWithTag("Player").transform.position = GameData.Instance.PlayerPosition.Value;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(System.DateTime.Now.Ticks - StartTime.Value >= EndTicks)
            {
                SceneManager.LoadScene("EndScene");
            }
        }

        public void ExitToDreamworld()
        {
            if (ZonePlayerIsIn != null)
                GameData.Instance.LastZone = ZonePlayerIsIn.Zone;
            else
                GameData.Instance.LastZone = ZoneEnvironment.EmptyRoom;

            GameData.Instance.PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

            switch (GameData.Instance.LastZone)
            {
                case ZoneEnvironment.MathRoom:
                    SceneManager.LoadScene("DreamSceneMath");
                    break;
                default:
                    SceneManager.LoadScene(GameData.DreamworldSceneName);
                    break;
            }

            
        }

        private void ResolveBattleResult()
        {
            bool battleResult = GameData.Instance.BattleResult.Value;

            Debug.Log(battleResult.ToString() + "the battle");

            if(battleResult)
            {
                //won, recover tiredness
                GameData.Instance.PlayerEnergy += GameData.PlayerMaxEnergy * GameData.PlayerWinRecoverFrac;
                GameData.Instance.PlayerEnergy = Mathf.Clamp(GameData.Instance.PlayerEnergy, 0, GameData.PlayerMaxEnergy);
            }
            else
            {
                GameData.Instance.PlayerEnergy += GameData.PlayerMaxEnergy * GameData.PlayerLoseRecoverFrac;
                GameData.Instance.PlayerEnergy = Mathf.Clamp(GameData.Instance.PlayerEnergy, 0, GameData.PlayerMaxEnergy);
            }

        }
    }
}