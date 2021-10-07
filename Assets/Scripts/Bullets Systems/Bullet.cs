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

    private void OnEnable()
    {
        EventBus.Subscribe(GameEvent.BASEDESTROYED, Hit);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(GameEvent.BASEDESTROYED, Hit);
    }
    public void Init(Transform _origin, Transform _target)
    {
        origin = _origin;
        target = _target;

        isActive = true;
        ResetPosition();
        Shoot();
    }

    private void ResetPosition()
    {
        this.transform.position = origin.position;
        this.transform.rotation = origin.rotation;
    }


    public void Hit()
    {
        ServiceLocator.GetService<BulletPool>().AddToPool(this.gameObject);
    }


    public abstract void Shoot();
}