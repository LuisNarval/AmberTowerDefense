using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is an scriptable object that can be created from the editor.
/// The porpuse of this script is to keep a Dictionary that is gonna serve as a relationship between the enemies prefabs & the enemies string ID.
/// The enemy factory asks for an prefab using a string ID, the scriptable object take this strings and return a prefab.
/// The list of enemies prefabs can be configurated from the editor, and then you can injected it in the constructor class of the Enemy Factory.
/// </summary>

[CreateAssetMenu(menuName = "Amber/Enemy Configuration")]
public class EnemyConfiguration : ScriptableObject
{
    [SerializeField] public Enemy[] enemies;

    private Dictionary<string, Enemy> idToEnemy;


    private void Awake()
    {
        idToEnemy = new Dictionary<string, Enemy>();

        foreach (var enemy in enemies)
        {
            idToEnemy.Add(enemy.ID, enemy);
        }
    }

    public Enemy GetEnemyPrefabByID(string _ID)
    {
        if (!idToEnemy.TryGetValue(_ID, out var enemy))
        {
            throw new Exception("Enemy with ID {ID} does not exist");
        }

        return enemy;
    }
}