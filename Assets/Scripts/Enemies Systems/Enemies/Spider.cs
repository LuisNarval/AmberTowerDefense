using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the implementation for an enemy Spider
/// The class inherits from Enemy
/// </summary>

public class Spider : Enemy
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