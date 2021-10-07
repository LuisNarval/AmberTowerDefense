using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The porpuse of this script is to serve as a bridge between the Gameplay scene & the Levels scenes.
/// Inside each Level scene exist a GameObject named LEVELINFO.
/// That GameObject has a script named LevelInfo.cs, it is a Monobehaviour who has all the LevelConfigurations & SpawnPositions neccesary for
/// the game to work. When that scripts awakes is gonna call SetLevelNavigation() & will subscribe this LevelSystem to the Service Locator.
/// After that, WaveSystme will be able to call this service & obtain the LevelConfiguration & the important positions for the level to work.
/// </summary>

public class LevelSystem
{
    private LevelConfiguration levelConfiguration;
    private Transform basePosition;
    private Transform[] spawnPositions;

    public Transform GetBasePosition()
    {
        return basePosition;
    }
    public Transform[] GetSpawnPositions()
    {
        return spawnPositions;
    }

    public LevelConfiguration GetLevelConfiguration()
    {
        return levelConfiguration;
    }

    public void SetLevelNavigation(Transform _basePosition, Transform[] _spawnPositions)
    {
        basePosition = _basePosition;
        spawnPositions = _spawnPositions;
    }
 
    public void UpdateLevelConfiguration(LevelConfiguration _levelConfiguration)
    {
        levelConfiguration = _levelConfiguration;
    }

}