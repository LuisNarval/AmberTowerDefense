using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is the parent class of all enemies entitys
/// This script uses the Template Method Design Pattern.
/// </summary>

public abstract class Enemy : MonoBehaviour
{
    public abstract void Move();

    public abstract void Search();

    public abstract void Atack();

    public abstract void ReceiveDamage();

}