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
    protected ITowerState currentState;
   
    public void SetInLand(Vector3 _position)
    {
        transform.position = _position;
        Init();
    }

    public void Init()
    {
        SetInitialState();
    }


    public abstract void SetInitialState();
    public abstract void ChangeState(ITowerState _towerState);

}