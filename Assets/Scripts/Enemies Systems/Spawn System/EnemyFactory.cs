using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This is the Enemy Factory Class, it uses the Factory Method Design Pattern.
/// The objetive of this class is to create the asked Enemy, no matter what type it is.
/// It uses an Enemy list, a dictionary & a string ID to keep control of all types of enemies in the project. 
/// </summary>

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;

    private Dictionary<string, Enemy> idToEnemy;


    private void Awake()
    {
        idToEnemy = new Dictionary<string, Enemy>();

        foreach(var enemy in enemies)
        {
            idToEnemy.Add(enemy.ID, enemy);
        }
    }


    public Enemy Create(string ID)
    {
        if(!idToEnemy.TryGetValue(ID, out var enemy))
        {
            throw new Exception("Enemy with ID {ID} does not exist");
        }

        return Instantiate(enemy);
    }



}
