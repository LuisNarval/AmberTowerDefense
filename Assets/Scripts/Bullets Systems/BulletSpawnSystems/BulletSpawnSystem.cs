using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the Bullet Spawn System.
/// His responsability is to Turn On the bullets pools & ask them to spawn an bullet each time a Tower need it.
/// The Bullet Spawn System also register the Bullet Pool to the Service Locator, this enables every bullet that has been spawned to return to 
/// the pool when they Hit something.
/// </summary>

public class BulletSpawnSystem : MonoBehaviour
{
    [SerializeField] private BulletConfiguration bulletConfiguration;

    private void Awake()
    {
    }


    private void Start()
    {
        EventBus.Subscribe(GameEvent.STARTGAME, Init);
    }

    void Init()
    {
        Debug.Log("Wake Spawn Bullet");
        InitPool(bulletConfiguration, 20);
    }

    public void InitPool(BulletConfiguration _bulletConfiguration, int _defaultPoolSize)
    {
        Debug.Log("InitPool");
        BulletPool bulletPool = new BulletPool(_bulletConfiguration, _defaultPoolSize);
        ServiceLocator.RegisterService(bulletPool);

        bulletPool.Init();
    }

    public void SpawnBullet(string _type, Transform _origin, Transform _destiny)
    {
        GameObject spawn = ServiceLocator.GetService<BulletPool>().PullObject(_type);
        spawn.GetComponent<Bullet>().Init(_origin, _destiny);
    }

}