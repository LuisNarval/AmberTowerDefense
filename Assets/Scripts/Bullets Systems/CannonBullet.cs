using System.Collections;
using UnityEngine;
/// <summary>
/// This Class is the implementation of the Dragon Bullet
/// </summary>
public class CannonBullet : Bullet
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
