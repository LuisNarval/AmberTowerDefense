using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This script is the parent class of all enemies entitys
/// This script uses the Template Method Design Pattern.
/// </summary>

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Transform destination;

    private void Awake()
    {
        Init();
    }

    public abstract void Init();

    public abstract void Move();

    public abstract void Search();

    public abstract void Atack();

    public abstract void ReceiveDamage();

}