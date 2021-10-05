using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private EnemyConfiguration enemyConfiguration;

    public void Awake()
    {
        EventBus.Subscribe(GameEvent.SPAWN, SpawnEnemy);

        EnemyFactory enemyFactory = new EnemyFactory(Instantiate(enemyConfiguration));
        ServiceLocator.RegisterService(enemyFactory);
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPool.Pull(),Vector3.zero, Quaternion.identity);
        Debug.Log("A Spider was Instantiated");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ServiceLocator.GetService<EnemyFactory>().Create("Spider");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ServiceLocator.GetService<EnemyFactory>().Create("Prototype");
        }

    }

}