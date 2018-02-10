using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I know it's a terrible name, I'm still half asleep

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
    public const float PlayerLoseRecoverFrac = 0.2f;
    public const float PlayerWinRecoverFrac = 1.0f;

    public const string DreamworldSceneName = "BattleTestScene";
    public const string OverworldSceneName = "OverworldTestScene";

    private static GameData ActualInstance;

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

    }

    //actual properties
    public float PlayerEnergy { get; set; }
    public SceneType LastScene { get; set; }
    public ZoneEnvironment LastZone { get; set; }
    public bool? BattleResult { get; set; }

    public RealInventory inventory;
}