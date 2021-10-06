using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
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

    public void SetLevelNavigation(Transform _basePosition, Transform[] _spawnPositions)
    {
        basePosition = _basePosition;
        spawnPositions = _spawnPositions;
    }
   
}