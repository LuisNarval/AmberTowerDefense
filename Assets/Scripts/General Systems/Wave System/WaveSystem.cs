using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
public class WaveSystem : MonoBehaviour
{
    [SerializeField] private EnemyConfiguration enemyConfiguration;
    [SerializeField] private int enemyPoolSize = 10;

    [ReadOnly] private LevelConfiguration levelConfiguration;

    private Transform basePosition;
    private Transform[] spawnPositions;
    
    private SpawnSystem spawnSystem;

    private void Awake()
    {  
        EventBus.Subscribe(GameEvent.STARTGAME, Init);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventBus.Publish(GameEvent.STARTGAME);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnSomething();
        }
    }

    private void Init()
    {
        basePosition = ServiceLocator.GetService<LevelSystem>().GetBasePosition();
        spawnPositions = ServiceLocator.GetService<LevelSystem>().GetSpawnPositions();
        levelConfiguration = ServiceLocator.GetService<LevelSystem>().GetLevelConfiguration();

        spawnSystem = new SpawnSystem(enemyConfiguration, enemyPoolSize);
    }

    private void SpawnSomething()
    {
        int randomEnemy = (int)Random.Range(0.0f, enemyConfiguration.enemies.Length);
        int randomSpawnPosition = (int)Random.Range(0.0f, spawnPositions.Length);

        string enemyName = enemyConfiguration.enemies[randomEnemy].ID; 
        spawnSystem.SpawnEnemy(enemyName, spawnPositions[randomSpawnPosition], basePosition);
    }


}