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
        GetComponent<AudioSource>().clip = screamSFX;
        GetComponent<AudioSource>().Play();
    }

    public override void deathFXS()
    {

        GetComponent<AudioSource>().clip = explotionSFX;
        GetComponent<AudioSource>().Play();

    }

}