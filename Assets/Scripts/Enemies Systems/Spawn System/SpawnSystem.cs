using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private EnemyConfiguration enemyConfiguration;

    [SerializeField] private Transform castle;
    [SerializeField] private Transform spiderStartPosition;
    [SerializeField] private Transform prototypeStartPosition;

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

    private void Update()
    {
        SuperInput();
    }
    void SuperInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Super Spider!");
            GameObject spider = ServiceLocator.GetService<EnemyPool>().PullObject("Spider");
            spider.GetComponent<Enemy>().Init(spiderStartPosition, castle);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Super Prototype!");
            GameObject prototype = ServiceLocator.GetService<EnemyPool>().PullObject("Prototype");
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
                    ServiceLocator.GetService<EnemyPool>().AddToPool(obj);
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
                    ServiceLocator.GetService<EnemyPool>().AddToPool(obj);
                }
            }
        }
    }


}