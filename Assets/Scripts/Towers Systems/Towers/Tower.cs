using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is the parent class of all towers entities
/// This script uses the Template Method Design Pattern, declaring general functions & leaving individual behaviour to his children.
/// This script also implement the State Desing Pattern, each behaviour State is declared in other class that implemente the ITowerState
/// interface. In runtime this components are atached to the tower & are used depending of the context.
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public abstract class Tower : MonoBehaviour
{
    [SerializeField] public string ID;
    [SerializeField] public string bulletID;
    [SerializeField] public float turnSpeed;
    [SerializeField] public float shootRate;
    [SerializeField] public Transform weaponPivot;
    [SerializeField] public Transform bulletPivot;

    public Transform currentObjective;
    private ITowerState currentState;
    private AtackTowerState AtackState;
    private SearchTowerState SearchState;
    private Transform position;

    public void Awake()
    {
        AtackState = gameObject.AddComponent<AtackTowerState>();
        SearchState = gameObject.AddComponent<SearchTowerState>();
    }

    private void OnEnable()
    {
        Init();
    }

    public void SetInLand(Transform _position)
    {
        position = _position;
    }

    public void Init()
    {
        Search();
    }

    public void Search()
    {
        ChangeState(SearchState);
    }

    public void Atack(Transform objective)
    {
        currentObjective = objective;
        ChangeState(AtackState);
    }

    public void ChangeState(ITowerState _towerState)
    {
        if (currentState != null)
        {
            currentState.DisHandle();
        }

        currentState = _towerState;
        currentState.Handle(this);
    }

    public abstract void TakeDamage();
}
