using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Overworld
{

    public class OverworldSceneController : MonoBehaviour
    {

        public ZoneController ZonePlayerIsIn; //this should probably be encapsulated but Unity hates proper ooop

        void Awake()
        {
            VideoModeManager.Init();
            VideoModeManager.SetOverworld();

            
        }

        // Use this for initialization
        void Start()
        {
            if (GameData.Instance.BattleResult.HasValue)
                ResolveBattleResult();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ExitToDreamworld()
        {
            if (ZonePlayerIsIn != null)
                GameData.Instance.LastZone = ZonePlayerIsIn.Zone;

            SceneManager.LoadScene(GameData.DreamworldSceneName); 
        }

        private void ResolveBattleResult()
        {
            bool battleResult = GameData.Instance.BattleResult.Value;

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

            //reset fields
            GameData.Instance.BattleResult = null;
            GameData.Instance.LastScene = SceneType.OverworldScene;
            GameData.Instance.LastZone = ZoneEnvironment.EmptyRoom;

        }
    }
}