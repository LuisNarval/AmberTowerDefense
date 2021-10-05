using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size;
}


public class EnemyPool : MonoBehaviour
{
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;


    /*private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

    }*/



    public GameObject SpawnFromPool(string tag, Transform _origin, Transform _objective)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + "doesn't exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = _origin.position;
        objectToSpawn.transform.rotation = _origin.rotation;

        objectToSpawn.SetActive(true);

        objectToSpawn.GetComponent<Enemy>().Init( _origin, _objective);

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }



}