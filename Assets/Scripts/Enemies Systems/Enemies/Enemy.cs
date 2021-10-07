using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// This script is the parent class of all enemies entities
/// This script uses the Template Method Design Pattern, declaring general functions & leaving individual behaviour to his children.
/// This script also implement the State Desing Pattern, each behaviour State is declared in other class that implemente the IEnemyState
/// interface. In runtime this components are atached to the enemy & are used depending of the context.
/// </summary>

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public string ID;
    [SerializeField] public float Life;
    [SerializeField] public Image lifeBar;
    [SerializeField] public string bulletID;
    [SerializeField] public float shootRate;
    [SerializeField] public Transform shootPivot;
    [HideInInspector] public Animator animator;

    public Transform target;
    public bool isActive;
    private bool isAtacking;

    protected float currentLife;
    protected Transform destination;
    protected Transform origin;
    protected NavMeshAgent navMeshAgent;
    
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe(GameEvent.BASEDESTROYED, Stop);
        isAtacking = false;
        isActive = true;
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(GameEvent.BASEDESTROYED, Stop);
        isActive = false;
    }

    public void Init(Transform _startPosition, Transform _destination)
    {
        origin = _startPosition;
        destination = _destination;
        currentLife = Life;
        ResetPosition();
        Move();
    }

    public void Move()
    {
        navMeshAgent.destination = destination.position;
        navMeshAgent.isStopped = false;
        animator.SetBool("isMoving", true);
    }

    public void Stop()
    {
        if (!isAtacking)
        {
            navMeshAgent.isStopped = true;
            animator.SetBool("isMoving", false);
        }
        else
        {
            StopAtack();
        }
    }

    public void ResetPosition()
    {
        navMeshAgent.Warp(new Vector3(origin.position.x, this.transform.position.y, origin.position.z));
        this.transform.rotation = origin.rotation;
    }

    public void SetLifeBar()
    {
        lifeBar.fillAmount = currentLife / Life;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TowerBullet"))
        {
            other.GetComponent<Bullet>().Hit();
            TakeDamage(other.GetComponent<Bullet>().damage);
        }

        if (other.CompareTag("Base"))
        {
            Stop();
            this.transform.LookAt(other.transform);
            target = other.transform;
            isAtacking = true;
            Atack();
        }
    }

    void TakeDamage(float _damage)
    {
        currentLife -= _damage;
        SetLifeBar();

        if (currentLife < 0)
            Die();

        HitFXS();
    }

    void Die()
    {
        ResetPosition();
        DeathFXS();
        StopAtack();
        Invoke("Pool", .2f);
    }

    void Pool()
    {
        ServiceLocator.GetService<EnemyPool>().AddToPool(this.gameObject);
    }


    public abstract void Atack();
    public abstract void StopAtack();
    public abstract void HitFXS();
    public abstract void DeathFXS();

}