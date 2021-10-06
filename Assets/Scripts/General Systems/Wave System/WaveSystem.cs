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
    }

    private void Init()
    {
        basePosition = ServiceLocator.GetService<LevelSystem>().GetBasePosition();
        spawnPositions = ServiceLocator.GetService<LevelSystem>().GetSpawnPositions();
        levelConfiguration = ServiceLocator.GetService<LevelSystem>().GetLevelConfiguration();

        spawnSystem = new SpawnSystem(enemyConfiguration, enemyPoolSize);

        StartCoroutine(SendWaves());
    }

    private void SpawnSomething(int _enemyNumber)
    {
        int randomSpawnPosition = (int)Random.Range(0.0f, spawnPositions.Length);

        string enemyName = enemyConfiguration.enemies[_enemyNumber].ID; 
        spawnSystem.SpawnEnemy(enemyName, spawnPositions[randomSpawnPosition], basePosition);
    }


    IEnumerator SendWaves()
    {
        Wave[] waves = levelConfiguration.waves;

        int totalWaves = waves.Length;
        int currentWave = 0;

        
        while (currentWave < totalWaves)
        {
            int enemiesForThisWave = waves[currentWave].WaveEnemies;
            int enemiesSpawned = 0;

            int typesOfEnemies = waves[currentWave].EnemyRate.Length;

            float timeBetweenSpawn = waves[currentWave].WaveDuration/ waves[currentWave].WaveEnemies;

            Debug.Log("Wave number: " + currentWave);
            while (enemiesSpawned < enemiesForThisWave)
            {
                int randomEnemy = (int)Random.Range(0.0f, typesOfEnemies);
                SpawnSomething(randomEnemy);
                enemiesSpawned++;

                yield return new WaitForSeconds(timeBetweenSpawn);
            }

            currentWave++;
        }

        Debug.Log("WAVES ENDED.");
    }

}