using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private LuisPool luisPool;
    [SerializeField] private SuperPool superPool;

    [SerializeField] private EnemyConfiguration enemyConfiguration;

    [SerializeField] private Transform castle;

    [SerializeField] private Transform spiderStartPosition;
    [SerializeField] private Transform prototypeStartPosition;

    public void Awake()
    {
        EventBus.Subscribe(GameEvent.SPAWN, SpawnEnemy);

        EnemyFactory enemyFactory = new EnemyFactory(Instantiate(enemyConfiguration));
        ServiceLocator.RegisterService(enemyFactory);
    }

    public void SpawnEnemy()
    {
        //Instantiate(enemyPool.Pull(),Vector3.zero, Quaternion.identity);
        Debug.Log("A Spider was Instantiated");
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ServiceLocator.GetService<EnemyFactory>().Create("Spider");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ServiceLocator.GetService<EnemyFactory>().Create("Prototype");
        }



        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Spider!");
            enemyPool.SpawnFromPool("Spider", spiderStartPosition, castle);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Prototype!");
            enemyPool.SpawnFromPool("Prototype", prototypeStartPosition, castle);
        }

        */

        LuisInput();
        SuperInput();
    }

    void LuisInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Luis Spider!");
            GameObject spider = luisPool.PullObject("Spider");
            spider.GetComponent<Enemy>().Init(spiderStartPosition, castle);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Luis Prototype!");
            GameObject prototype = luisPool.PullObject("PrototypeEnemy");
            prototype.GetComponent<Enemy>().Init(prototypeStartPosition, castle);
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            object[] objs = GameObject.FindObjectsOfType(typeof(GameObject));
            foreach (object o in objs)
            {
                GameObject obj = (GameObject)o;

                if (obj.gameObject.GetComponent<Spider>() != null)
                {
                    luisPool.PoolObject(obj);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            object[] objs = GameObject.FindObjectsOfType(typeof(GameObject));
            foreach (object o in objs)
            {
                GameObject obj = (GameObject)o;

                if (obj.gameObject.GetComponent<PrototypeEnemie>() != null)
                {
                    luisPool.PoolObject(obj);
                }
            }
        }
        
    }


    void SuperInput()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            superPool.Init();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Super Spider!");
            GameObject spider = superPool.PullObject("Spider");
            spider.GetComponent<Enemy>().Init(spiderStartPosition, castle);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Super Prototype!");
            GameObject prototype = superPool.PullObject("Prototype");
            prototype.GetComponent<Enemy>().Init(prototypeStartPosition, castle);
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            object[] objs = GameObject.FindObjectsOfType(typeof(GameObject));
            foreach (object o in objs)
            {
                GameObject obj = (GameObject)o;

                if (obj.gameObject.GetComponent<Spider>() != null)
                {
                    superPool.PoolObject(obj);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            object[] objs = GameObject.FindObjectsOfType(typeof(GameObject));
            foreach (object o in objs)
            {
                GameObject obj = (GameObject)o;

                if (obj.gameObject.GetComponent<PrototypeEnemie>() != null)
                {
                    superPool.PoolObject(obj);
                }
            }
        }
    }




}