using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is an scriptable object that can be created from the editor.
/// The porpuse of this script is to keep a Dictionary that is gonna serve as a relationship between the towers prefabs & the towers string ID.
/// The tower factory asks for an prefab using a string ID, the scriptable object take this strings and return a prefab.
/// The list of towers prefabs can be configurated from the editor, and then you can injected it in the constructor class of the Tower Factory.
/// </summary>

[CreateAssetMenu(menuName = "Amber/Tower Configuration")]
public class TowerConfiguration : ScriptableObject
{
    [SerializeField] public Tower[] towers;

    private Dictionary<string, Tower> idToTower;


    private void Awake()
    {
        idToTower = new Dictionary<string, Tower>();

        foreach (var tower in towers)
        {
            idToTower.Add(tower.ID, tower);
        }
    }

    public Tower GetTowerPrefabByID(string _ID)
    {
        if (!idToTower.TryGetValue(_ID, out var tower))
        {
            throw new Exception("Tower with ID {ID} does not exist");
        }

        return tower;
    }
}
