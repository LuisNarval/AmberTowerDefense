using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This script is the parent class of all enemies entitys
/// This script uses the Template Method Design Pattern.
/// </summary>

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public string ID;

    [SerializeField] protected Transform destination;   
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Animator animator;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        destination = this.transform;
        Move();
    }

    public void Move()
    {
        navMeshAgent.destination = destination.position;
        animator.SetBool("IsMoving", true);
    }

    public void Stop()
    {
        navMeshAgent.destination = destination.position;
        animator.SetBool("IsMove", false);
    }

    public abstract void Search();

    public abstract void Atack();

    public abstract void TakeDamage();

}