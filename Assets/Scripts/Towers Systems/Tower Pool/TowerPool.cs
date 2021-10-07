using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// This is the Object Pool for the towers.
/// We are using this Design Pattern so we can improve optimization.
/// The idea behind this pattern is to recycle the objects that alredy live in the scene instead of constantly intantiate them.
/// This script can Pull an object from the pool and if is necesary, it can later Return it to the pool.
/// When we run out of objects, the tower Pool calls to the Tower Factory, so this class create a new Tower for the pool.
/// </summary>

public class TowerPool
{
    private string[] objects;
    private List<GameObject>[] pooledObjects;

    private int[] amountToBuffer;
    private int defaultBufferAmount = 5;
    private GameObject containerObject;

    private TowerConfiguration towerConfig;
    private TowerFactory towerFactory;

    public TowerPool(TowerConfiguration _towerConfiguration, int _defaultBufferAmount)
    {
        towerFactory = new TowerFactory(Object.Instantiate(_towerConfiguration));

        towerConfig = _towerConfiguration;
        defaultBufferAmount = _defaultBufferAmount;
    }

    public void Init()
    {
        RegysterTowerTypes();
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
                    Tower tower = towerFactory.Create(objects[i]);
                    return tower.gameObject;
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


    private void RegysterTowerTypes()
    {
        int amountOfTypes = towerConfig.towers.Length;
        objects = new string[amountOfTypes];
        amountToBuffer = new int[amountOfTypes];

        for (int i = 0; i < towerConfig.towers.Length; i++)
        {
            objects[i] = towerConfig.towers[i].ID;
            amountToBuffer[i] = defaultBufferAmount;
        }
    }


    private void CreateDefaultObjects()
    {
        containerObject = new GameObject("TowerPool");
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
                Tower newObj = towerFactory.Create(obj);
                newObj.gameObject.name = obj;
                AddToPool(newObj.gameObject);
            }

            i++;
        }
    }



}
