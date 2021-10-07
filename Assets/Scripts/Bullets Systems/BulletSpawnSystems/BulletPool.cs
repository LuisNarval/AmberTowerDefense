using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// This is the Object Pool for the bullets.
/// We are using this Design Pattern so we can improve optimization.
/// The idea behind this pattern is to recycle the objects that alredy live in the scene instead of constantly intantiate them.
/// This script can Pull an object from the pool and if is necesary, it can later Return it to the pool.
/// When we run out of objects, the Bullet Pool calls to the Bullet Factory, so this class create a new Bullet for the pool.
/// </summary>

public class BulletPool
{
    private string[] objects;
    private List<GameObject>[] pooledObjects;

    private int[] amountToBuffer;
    private int defaultBufferAmount = 5;
    private GameObject containerObject;

    private BulletConfiguration bulletConfig;
    private BulletFactory bulletFactory;

    public BulletPool(BulletConfiguration _bulletConfiguration, int _defaultBufferAmount)
    {
        bulletFactory = new BulletFactory(Object.Instantiate(_bulletConfiguration));

        bulletConfig = _bulletConfiguration;
        defaultBufferAmount = _defaultBufferAmount;
    }

    public void Init()
    {
        RegysterEnemyTypes();
        CreateDefaultObjects();
    }


    public GameObject PullObject(string _objectType)
    {
        bool onlyPooled = false;

        for (int i = 0; i < objects.Length; i++)
        {
            string prefab = objects[i];

            if (prefab == _objectType)
            {
                if (pooledObjects[i].Count > 0)
                {
                    GameObject pooledObject = pooledObjects[i][0];
                    pooledObject.SetActive(true);
                    //pooledObject.transform.parent = null;

                    pooledObjects[i].Remove(pooledObject);

                    return pooledObject;
                }
                else if (!onlyPooled)
                {
                    Bullet bullet = bulletFactory.Create(objects[i]);
                    return bullet.gameObject;
                }

                break;
            }
        }

        return null;
    }

    public void AddToPool(GameObject _obj)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] == _obj.name)
            {
                _obj.SetActive(false);
                _obj.transform.parent = containerObject.transform;
                pooledObjects[i].Add(_obj);
                return;
            }
        }

        Object.Destroy(_obj);
    }


    private void RegysterEnemyTypes()
    {
        int amountOfTypes = bulletConfig.bullets.Length;
        objects = new string[amountOfTypes];
        amountToBuffer = new int[amountOfTypes];

        for (int i = 0; i < bulletConfig.bullets.Length; i++)
        {
            objects[i] = bulletConfig.bullets[i].ID;
            amountToBuffer[i] = defaultBufferAmount;
        }
    }


    private void CreateDefaultObjects()
    {
        containerObject = new GameObject("BulletPool");
        pooledObjects = new List<GameObject>[objects.Length];

        int i = 0;

        foreach (string obj in objects)
        {
            pooledObjects[i] = new List<GameObject>();
            int bufferAmount;

            if (i < amountToBuffer.Length)
            {
                bufferAmount = amountToBuffer[i];
            }
            else
            {
                bufferAmount = defaultBufferAmount;
            }

            for (int n = 0; n < bufferAmount; n++)
            {
                Bullet newObj = bulletFactory.Create(obj);
                newObj.gameObject.name = obj;
                AddToPool(newObj.gameObject);
            }

            i++;
        }
    }

}