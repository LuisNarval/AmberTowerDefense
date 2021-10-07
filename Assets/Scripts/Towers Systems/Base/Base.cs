using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script manage the life of the base.
/// When the base life reaches zero the scripts calls the Event Bus telling it that the base was destroyed.
/// </summary>

public class Base : MonoBehaviour
{
    [SerializeField] float life;
    [SerializeField] Image lifeBar;
    [SerializeField] GameObject explotion;

    private float currentLife;

    private void Awake()
    {
        currentLife = life;
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            currentLife -= collision.gameObject.GetComponent<Bullet>().damage;
            updateLifeBar();
            ServiceLocator.GetService<BulletPool>().AddToPool(collision.gameObject);

            if (currentLife <= 0)
                destroyBase();
        }
    }


    void updateLifeBar()
    {
        lifeBar.fillAmount = currentLife / life;
    }

    void destroyBase()
    {
        Instantiate(explotion, this.transform.position, Quaternion.identity);
        EventBus.Publish(GameEvent.BASEDESTROYED);
        Destroy(this.gameObject);
    }

}