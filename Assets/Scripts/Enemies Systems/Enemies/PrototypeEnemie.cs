using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This is the prototype for an enemy implementation
/// </summary>

public class PrototypeEnemie : Enemy
{
    [SerializeField] public AudioClip screamSFX;
    [SerializeField] public AudioClip explotionSFX;

    private IEnemyState currentState;
    private EnemyAtackState AtackState;
    public override void Atack()
    {
        AtackState = gameObject.AddComponent<EnemyAtackState>();
        currentState = AtackState;
        currentState.Handle(this);
    }

    public override void StopAtack()
    {
        if (currentState != null)
        {
            currentState.DisHandle();
        }
    }

    public override void HitFXS()
    {
        GetComponent<AudioSource>().clip = screamSFX;
        GetComponent<AudioSource>().Play();
    }

    public override void DeathFXS()
    {
    }


}