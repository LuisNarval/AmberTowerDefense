using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the Tower Spawn System.
/// His responsability is to Turn On the tower pools & ask them to spawn an tower each time the user need it.
/// The Tower Spawn System also register the Tower Pool to the Service Locator, this enables every enemy that has been spawned to return to 
/// the pool when they are destroyed.
/// </summary>

public class TowerSpawnSystem 
{
    public TowerSpawnSystem(TowerConfiguration _towerConfiguration, int _defaultPoolSize)
    {
        TowerPool towerPool = new TowerPool(_towerConfiguration, _defaultPoolSize);
        ServiceLocator.RegisterService(towerPool);

        towerPool.Init();
    }

    public void SpawnTower(string _type, Transform _position)
    {
        GameObject spawn = ServiceLocator.GetService<TowerPool>().PullObject(_type);
        spawn.GetComponent<Tower>().SetInLand(_position);
    }

}
