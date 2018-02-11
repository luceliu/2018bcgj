using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I know it's a terrible name, I'm still half asleep
using UnityEngine.SceneManagement;

public enum SceneType
{
    OtherScene, OverworldScene, DreamworldScene
}

public enum ZoneEnvironment
{
    EmptyRoom, ComputerRoom, EnglishRoom, MathRoom
}

public class GameData
{
    //sticking these here BECAUSE WHY NOT
    public const float PlayerMaxEnergy = 100f;
    public const float PlayerSleepThresholdFrac = 0.25f;
    public const float PlayerLoseRecoverFrac = 0.2f;
    public const float PlayerWinRecoverFrac = 1.0f;

    public const string DreamworldSceneName = "DreamScene";
    public const string OverworldSceneName = "OverworldScene";

    private static GameData ActualInstance;
    public RealInventory CurrentInventory { get; private set; }

    //I just hacked around it
    public InventoryPanel inventoryPanel { get { return GameObject.Find("WorldRoot").GetComponentInChildren<InventoryPanel>(); } }
    public DialogPanel dialogPanel { get { return GameObject.Find("DialogPanel").GetComponent<DialogPanel>(); } } 
    public static GameData Instance
    {
        get
        {
            if (ActualInstance == null)
                ActualInstance = new GameData();

            return ActualInstance;
        }
    }

    public GameData()
    {
        CurrentInventory = ScriptableObject.CreateInstance<RealInventory>();
        PlayerEnergy = PlayerMaxEnergy;
        TookMelatonin = false;

        //YOU CANNOT DO THIS I SPECIFICALLY CAUTIONED AGAINST IT
        //inventoryPanel = GameObject.Find("Canvas").GetComponent<InventoryPanel>();
        //dialogPanel = GameObject.Find("DialogPanel").GetComponent<DialogPanel>();
    }

    //actual properties
    public float PlayerEnergy { get; set; }
    public SceneType LastScene { get; set; }
    public ZoneEnvironment LastZone { get; set; }
    public bool? BattleResult { get; set; }

    public bool TookMelatonin { get; set; }
}