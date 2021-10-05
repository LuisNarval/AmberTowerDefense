using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : Singleton<SpawnSystem>
{
    [SerializeField] private GameObject Spider;

    public override void Awake()
    {
        
    }

    public GameObject Pull()
    {
        return Spider;
    }  


}