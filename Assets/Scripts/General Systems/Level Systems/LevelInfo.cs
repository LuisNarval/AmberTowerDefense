using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This scripts lives in the Levels Scenes & keep a reference to the LevelConfiguration & other importan positions.
/// This will subscribe a LevelSystem to the Service locator & will share with it his Level Navigation Info.
/// After that, the Wave system will be able to read all this info & use it to spawn the specifics enemies waves for the current level.
/// </summary>
public class LevelInfo : MonoBehaviour
{
    [Header("REFERENCE")]
    public LevelConfiguration levelConfiguration;
    public Transform basePosition;
    public Transform[] spawnPositions;

    private void Awake()
    {
        ServiceLocator.RegisterService(new LevelSystem());
        ServiceLocator.GetService<LevelSystem>().SetLevelNavigation(basePosition, spawnPositions);
        ServiceLocator.GetService<LevelSystem>().UpdateLevelConfiguration(levelConfiguration);
    }

}