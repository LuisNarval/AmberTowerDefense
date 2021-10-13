using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// I'm using a multiscene pipeline, so this Scene Configuration is gonna help to keep a record of wich scenes makes certain level.
/// For this prototype purpuse, we are only using 2 scenes per level. The Gameplay who has de logic and the scene level that has the
/// enviroment & ilumination
/// /// </summary>


[System.Serializable]
public class Level
{
    public string LevelName;
    public string[] Scene; 
}


[CreateAssetMenu(menuName = "Amber/Scene Configuration")]
public class SceneConfiguration : ScriptableObject
{
    public string SceneConfigurationName;
    public Level[] Level;   
}