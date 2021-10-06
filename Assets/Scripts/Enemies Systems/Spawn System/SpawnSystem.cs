using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the Spawn System.
/// His responsability is to Turn On the enemies pools & ask them to spawn an enemy each time the Wave System need it.
/// The Spawn System also register the Enemy Pool to the Service Locator, this enables every enemy that has been spawned to return to 
/// the pool when they Die.
/// </summary>
public class SpawnSystem
{
    public SpawnSystem(EnemyConfiguration _enemyConfiguration, int _defaultPoolSize)
    {
        EventBus.Subscribe(GameEvent.STARTGAME, InitPool);

        EnemyPool enemyPool = new EnemyPool(_enemyConfiguration, _defaultPoolSize);
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