using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// This is the Object Pool for the enemies.
/// We are using this Design Pattern so we can improve optimization.
/// The idea behind this pattern is to recycle the objects that alredy live in the scene instead of constantly intantiate them.
/// This script can Pull an object from the pool and if is necesary, it can later Return it to the pool.
/// When we run out of objects, the Enemy Pool calls to the Enemy Factory, so this class create a new Enemy for the pool.
/// </summary>

public class EnemyPool
{
    private string[] objects;
    private List<GameObject>[] pooledObjects;
    private int[] amountToBuffer;
    private int defaultBufferAmount = 5;
    private GameObject containerObject;
    private EnemyConfiguration enemyConfig;

    private EnemyFactory enemyFactory;
    public EnemyPool(EnemyConfiguration _enemyConfiguration, int _defaultBufferAmount)
    {
        enemyFactory = new EnemyFactory(Object.Instantiate(_enemyConfiguration));   

        enemyConfig = _enemyConfiguration;
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
                    Enemy enemy = enemyFactory.Create(objects[i]);
                    return enemy.gameObject;
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
        int amountOfTypes = enemyConfig.enemies.Length;
        objects = new string[amountOfTypes];
        amountToBuffer = new int[amountOfTypes];

        for (int i = 0; i < enemyConfig.enemies.Length; i++)
        {
            objects[i] = enemyConfig.enemies[i].ID;
            amountToBuffer[i] = defaultBufferAmount;
        }
    }


    private void CreateDefaultObjects()
    {
        containerObject = new GameObject("EnemyPool");
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
                Enemy newObj = enemyFactory.Create(obj);
                newObj.gameObject.name = obj;
                AddToPool(newObj.gameObject);
            }

            i++;
        }
    }



}