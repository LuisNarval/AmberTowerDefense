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
    [SerializeField] public GameObject hitFX;
    [SerializeField] public GameObject deathFX;

    public override void Search()
    {

    }

    public override void Atack()
    {

    }

    public override void hitFXS()
    {
        hitFX.SetActive(false);
        GetComponent<AudioSource>().clip = screamSFX;
        GetComponent<AudioSource>().Play();
        hitFX.SetActive(true);
    }

    public override void deathFXS()
    {
        deathFX.transform.parent = null;
        deathFX.SetActive(false);
        GetComponent<AudioSource>().clip = explotionSFX;
        GetComponent<AudioSource>().Play();
        deathFX.transform.position = this.transform.position;
        deathFX.SetActive(true);
    }


}