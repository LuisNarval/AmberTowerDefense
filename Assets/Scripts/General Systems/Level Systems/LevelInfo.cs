using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    [Header("REFERENCE")]
    public Transform basePosition;
    public Transform[] spawnPositions;

    private void Awake()
    {
        ServiceLocator.RegisterService(new LevelSystem());
        ServiceLocator.GetService<LevelSystem>().SetLevelNavigation(basePosition, spawnPositions);
    }

}