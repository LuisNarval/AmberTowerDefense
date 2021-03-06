using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is the ShadowBullet
/// It inherits from the Bullet Class.
/// This is the bullet that is gonna be used by the Phantom Enemy
/// </summary>
public class ShadowBullet : Bullet
{
    public override void Shoot()
    {
        StartCoroutine(FlyToEnemy());
    }

    IEnumerator FlyToEnemy()
    {
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * 2.0f;

            this.transform.LookAt(target);
            this.transform.position = Vector3.Lerp(this.transform.position, target.position, time);

            yield return new WaitForEndOfFrame();
        }
    }
}
