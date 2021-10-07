using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is an scriptable object that can be created from the editor.
/// The porpuse of this script is to keep a Dictionary that is gonna serve as a relationship between the bullets prefabs & the bullets string ID.
/// The bullet factory asks for an prefab using a string ID, the scriptable object take this strings and return a prefab.
/// The list of bullets prefabs can be configurated from the editor, and then you can injected it in the constructor class of the Bullet Factory.
/// </summary>

[CreateAssetMenu(menuName = "Amber/Bullet Configuration")]
public class BulletConfiguration : ScriptableObject
{
    [SerializeField] public Bullet[] bullets;

    private Dictionary<string, Bullet> idToBullet;


    private void Awake()
    {
        idToBullet = new Dictionary<string, Bullet>();

        foreach (var bullet in bullets)
        {
            idToBullet.Add(bullet.ID, bullet);
        }
    }

    public Bullet GetBulletPrefabByID(string _ID)
    {
        if (!idToBullet.TryGetValue(_ID, out var bullet))
        {
            throw new Exception("Enemy with ID {ID} does not exist");
        }

        return bullet;
    }
}
