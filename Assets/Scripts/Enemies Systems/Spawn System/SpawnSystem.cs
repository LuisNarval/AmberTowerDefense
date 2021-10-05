using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private EnemyFactory enemyFactory;

    public void Awake()
    {
        EventBus.Subscribe(GameEvent.SPAWN, SpawnEnemy);
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
            enemyFactory.Create("Spider");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            enemyFactory.Create("Prototype");
        }

    }

}