using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;


/// <summary>
/// This script is the parent for all Towers that can atack enemies
/// This class heritence from the Tower Class, that makes it able to be instantiate in a tower grid.
/// This is implementing the State Desing Pattern, each behaviour State is declared in other class that implemente the ITowerState
/// interface. In runtime this components are atached to the tower & are used depending of the context.
/// </summary>


public abstract class AtackTower : Tower
{
    [SerializeField] public string bulletID;
    [SerializeField] public float turnSpeed;
    [SerializeField] public float shootRate;

    [SerializeField] public Transform weaponPivot;
    [SerializeField] public Transform bulletPivot;

    [SerializeField, ReadOnly] public Transform currentObjective;

    protected ITowerState IAtackState;
    protected ITowerState ISearchState;


    public override void SetInitialState()
    {
        CreateStateComponents();
        Search();
    }

    public override void ChangeState(ITowerState _towerState)
    {
        if (currentState != null)
        {
            currentState.DisHandle();
        }

        currentState = _towerState;
        currentState.Handle(this);
    }

    public void Search()
    {
        ChangeState(ISearchState);
    }

    public void Atack(Transform objective)
    {
        currentObjective = objective;
        ChangeState(IAtackState);
    }

    public abstract void CreateStateComponents();   

}