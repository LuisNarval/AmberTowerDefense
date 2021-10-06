using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This script is the parent class of all enemies entities
/// This script uses the Template Method Design Pattern.
/// </summary>

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public string ID;
    [SerializeField] public bool isActive;

    [SerializeField] protected Transform destination;
    [SerializeField] protected Transform origin;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Animator animator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        isActive = true;
    }

    private void OnDisable()
    {
        isActive = false;
    }

    public void Init(Transform _startPosition, Transform _destination)
    {
        origin = _startPosition;
        destination = _destination;
        ResetPosition();
        Move();
    }

    public void Move()
    {
        navMeshAgent.destination = destination.position;
        navMeshAgent.isStopped = false;
        animator.SetBool("IsMoving", true);
    }

    public void Stop()
    {
        navMeshAgent.isStopped = true;
        animator.SetBool("IsMoving", false);
    }

    public void ResetPosition()
    {
        navMeshAgent.Warp(new Vector3(origin.position.x, this.transform.position.y, origin.position.z));
        this.transform.rotation = origin.rotation;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathZone"))
        {
            Die();
        }
    }

    public void Die()
    {
        ResetPosition();
        ServiceLocator.GetService<EnemyPool>().AddToPool(this.gameObject);
    }



    public abstract void Search();

    public abstract void Atack();

    public abstract void TakeDamage();

}