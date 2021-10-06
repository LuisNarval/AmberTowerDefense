using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is the parent class of all bullets entities
/// This script uses the Template Method Design Pattern, declaring general functions & leaving individual behaviour to his children.
/// </summary>

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] public string ID;
    [SerializeField] public float speed;
    [SerializeField] public float damage;

    [SerializeField] protected bool isActive;
    [SerializeField] protected Transform origin;
    [SerializeField] protected Transform target;

    public void Init(Transform _origin, Transform _target)
    {
        origin = _origin;
        target = _target;

        ResetPosition();
        isActive = true;
    }

    public void ResetPosition()
    {
        this.transform.position = origin.position;
        this.transform.rotation = origin.rotation;
    }

    private void Update()
    {
        if(isActive)
            Move();
    }

    public void Hit()
    {

    }


    public abstract void Move();
    public abstract void Aim();
    public abstract void Fly();

}