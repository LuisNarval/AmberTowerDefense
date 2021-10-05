using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : Singleton<SpawnSystem>
{
    [SerializeField] private EnemyPool enemyPool;

    public override void Awake()
    {
   
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPool.Pull(),Vector3.zero, Quaternion.identity);
    }


}