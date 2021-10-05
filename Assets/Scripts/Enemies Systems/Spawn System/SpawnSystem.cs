using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : Singleton<SpawnSystem>
{
    [SerializeField] private EnemyPool enemyPool;

    public override void Awake()
    {
        EventBus.Subscribe("Spawn", SpawnEnemy);
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPool.Pull(),Vector3.zero, Quaternion.identity);
        Debug.Log("A Spider was Instantiated");
    }


}