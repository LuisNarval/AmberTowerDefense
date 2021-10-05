using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPool : MonoBehaviour
{
    public string[] objects;

    public List<GameObject>[] pooledObjects;

    public int[] amountToBuffer;

    public int defaultBufferAmount = 3;

    protected GameObject containerObject;

    public void Init()
    {
        Debug.Log("SUPER INIT");

        containerObject = new GameObject("SuperPool");
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
                Enemy newObj = ServiceLocator.GetService<EnemyFactory>().Create(obj);
                newObj.gameObject.name = obj;
                PoolObject(newObj.gameObject);
            }

            i++;
        }

    }

    public GameObject PullObject(string objectType)
    {
        bool onlyPooled = false;

        for (int i = 0; i < objects.Length; i++)
        {
            string prefab = objects[i];

            if (prefab == objectType)
            {
                if (pooledObjects[i].Count > 0)
                {
                    GameObject pooledObject = pooledObjects[i][0];
                    pooledObject.SetActive(true);
                    pooledObject.transform.parent = null;

                    pooledObjects[i].Remove(pooledObject);

                    return pooledObject;
                }
                else if (!onlyPooled)
                {
                    Enemy enemy = ServiceLocator.GetService<EnemyFactory>().Create(objects[i]);
                    return enemy.gameObject;
                }

                break;
            }
        }

        return null;
    }

    public void PoolObject(GameObject obj)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] == obj.name)
            {
                obj.SetActive(false);
                obj.transform.parent = containerObject.transform;
                pooledObjects[i].Add(obj);
                return;
            }
        }

        Destroy(obj);
    }


}