using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtackState : MonoBehaviour, IEnemyState
{
    Enemy enemy;

    public void Handle(Enemy _enemy)
    {
        enemy = _enemy;
        StartCoroutine("Shoot");
    }
    public void DisHandle()
    {
        StopAllCoroutines();
    }


    IEnumerator Shoot()
    {
        while (true)
        {
            enemy.animator.SetTrigger("shoot");
            GameObject bullet = ServiceLocator.GetService<BulletPool>().PullObject(enemy.bulletID);
            bullet.GetComponent<Bullet>().Init(enemy.shootPivot, enemy.target);            
            yield return new WaitForSeconds(enemy.shootRate);
        }

    }



}