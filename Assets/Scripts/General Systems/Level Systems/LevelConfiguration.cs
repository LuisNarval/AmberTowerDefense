using UnityEngine;

/// <summary>
/// This is an scriptable object that can be created from the editor.
/// The porpuse of this script is to let the Game Designers to edit the waves from each level.
/// You can modify the number of waves, their duration, the number of enemies per wave & the spawn rate each has.
/// The scriptable object most to be reference in each Level Scene, in the LevelInfo MonoBehaviour script, so it can be read later for
/// the WaveSystem located in the Gameplay Scene.
/// </summary>

[System.Serializable]
public class Wave{
    [Tooltip("The wave duration in seconds.")]
    public int WaveDuration;
    [Tooltip("The number of enemies for this wave")]
    public int WaveEnemies;
    [Range(0.0f,1.0f)]
    [Tooltip("How much percentage of this wave will the specific enemy appear")]
    public float[] EnemyRate;
    [Tooltip("The wave has ended?. Just for the system purpuse.")]
    [Unity.Collections.ReadOnly]
    public bool WaveEnded;
}


[CreateAssetMenu(menuName = "Amber/Level Configuration")]
public class LevelConfiguration : ScriptableObject
{
    [SerializeField] public string levelName;
    [SerializeField] public Wave[] waves;
}