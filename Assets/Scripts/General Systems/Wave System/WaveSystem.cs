using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
public class WaveSystem : MonoBehaviour
{
    [SerializeField] private EnemyConfiguration enemyConfiguration;
    private LevelConfiguration levelConfiguration;

    private Transform basePosition;
    private Transform[] spawnPositions;
    
    private SpawnSystem spawnSystem;
    private int enemyPoolSize;

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
        enemyPoolSize = levelConfiguration.poolSize;

        spawnSystem = new SpawnSystem(enemyConfiguration, enemyPoolSize);

        StartCoroutine(SendWaves());
    }

    public int enemiesToSpawn;
    IEnumerator SendWaves()
    {
        Wave[] waves = levelConfiguration.waves;

        int totalWaves = waves.Length;
        int currentWave = 0;


        while (currentWave < totalWaves)
        {
            float timeBetweenSpawn = waves[currentWave].WaveDuration / waves[currentWave].WaveEnemies;
            enemiesToSpawn = waves[currentWave].WaveEnemies;

            Debug.Log("Wave number: " + currentWave);
            while (enemiesToSpawn > 0)
            {
                int randomEnemy = GetRandomEnemyByRate(waves[currentWave].EnemyRate);

                SpawnRandomEnemy(randomEnemy);
                enemiesToSpawn--;

                yield return new WaitForSeconds(timeBetweenSpawn);
            }

            Debug.Log("Wave spawn has ended. Waiting for enemies");

            while (isAnEnemyInScene())
            {
                yield return new WaitForSeconds(2.0f);
            }

            currentWave++;
        }

        Debug.Log("WAVES ENDED.");
    }

    

    private void SpawnRandomEnemy(int _enemyNumber)
    {
        int randomSpawnPosition = (int)Random.Range(0.0f, spawnPositions.Length);

        string enemyName = enemyConfiguration.enemies[_enemyNumber].ID;
        spawnSystem.SpawnEnemy(enemyName, spawnPositions[randomSpawnPosition], basePosition);
    }

    private int GetRandomEnemyByRate(float[] _Rate)
    {
        float totalBet = 0;

        for (int i = 0; i < _Rate.Length; i++)
        {
            totalBet += _Rate[i];
        }

        float outcome = Random.Range(0.0f, totalBet);
        float bag = 0;

        for (int i = 0; i < _Rate.Length; i++)
        {
            bag += _Rate[i];
            if (outcome < bag)
            {
                return (i);
            }

        }

        return 0;
    }

    private bool isAnEnemyInScene()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
        {
            if (enemy.isActive)
                return true;
        }

        return false;
    }

}