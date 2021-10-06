using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private EnemyConfiguration enemyConfiguration;

    public void Awake()
    {
        EventBus.Subscribe(GameEvent.STARTGAME,InitPool);

        EnemyPool enemyPool = new EnemyPool(enemyConfiguration, 5);
        ServiceLocator.RegisterService(enemyPool);
    }

    public void InitPool()
    {
        ServiceLocator.GetService<EnemyPool>().Init();
    }
    public void SpawnEnemy(string _type, Transform _origin, Transform _destiny)
    {
        GameObject spawn = ServiceLocator.GetService<EnemyPool>().PullObject(_type);
        spawn.GetComponent<Enemy>().Init(_origin, _destiny);
    }

}