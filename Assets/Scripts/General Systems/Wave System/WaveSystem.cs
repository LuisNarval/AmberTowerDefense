using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private EnemyConfiguration enemyConfiguration;
    [SerializeField] private int enemyPoolSize = 10;

    [SerializeField] private Transform destiny;
    [SerializeField] private Transform spiderOrigin;
    [SerializeField] private Transform prototypeOrigin;

    private SpawnSystem spawnSystem;

    private void Awake()
    {
        spawnSystem = new SpawnSystem(enemyConfiguration, enemyPoolSize);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventBus.Publish(GameEvent.STARTGAME);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            spawnSystem.SpawnEnemy("Spider", spiderOrigin, destiny);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            spawnSystem.SpawnEnemy("Prototype", prototypeOrigin, destiny);
        }

    }


}