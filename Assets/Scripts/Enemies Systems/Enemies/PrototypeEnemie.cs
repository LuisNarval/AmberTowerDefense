using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This is the prototype for an enemy implementation
/// </summary>

public class PrototypeEnemie : Enemy
{    
    public override void Init()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.destination = destination.position;
    }

    public override void Move()
    {

    }

    public override void Search()
    {

    }

    public override void Atack()
    {

    }

    public override void ReceiveDamage()
    {

    }

}