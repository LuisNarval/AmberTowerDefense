using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsable for managing the Atack State of the towers.
/// It implements the ITowerState interface, following the State Design Pattern
/// Aims to the target and call the bullet pool for instantiating certain bullet;
/// </summary>

public class AtackTowerState : MonoBehaviour, ITowerState
{
    Tower tower;
    bool stateActive = false;


    public void Handle(Tower _tower)
    {
        tower = _tower;
        stateActive = true;
        StartCoroutine("Aim");
    }
    public void DisHandle()
    {
        stateActive = false;
        StopAllCoroutines();
    }

    IEnumerator Aim()
    {
        float timeToAim = 0.0f;

        Quaternion originalRotation = tower.weaponPivot.transform.rotation;
        Quaternion lookAtRotation;
        
        while (timeToAim < 1.0f)
        {
            yield return new WaitForEndOfFrame();
            tower.weaponPivot.transform.LookAt(tower.currentObjective);
            lookAtRotation = tower.weaponPivot.transform.rotation;

            tower.weaponPivot.transform.rotation = Quaternion.Lerp(originalRotation, lookAtRotation, timeToAim);
            timeToAim += Time.deltaTime * 2;
        }

        StartCoroutine("Shoot");

        while (true)
        {
            tower.weaponPivot.transform.LookAt(tower.currentObjective);
            yield return new WaitForEndOfFrame();
        }
    }


    IEnumerator Shoot()
    {
        while (true)
        {
            GameObject bullet = ServiceLocator.GetService<BulletPool>().PullObject("ArrowBullet");
            bullet.GetComponent<Bullet>().Init(tower.bulletPivot, tower.currentObjective);

            yield return new WaitForSeconds(tower.shootRate);
        }
    }




    public void OnTriggerExit(Collider other)
    {
        if (stateActive)
        {
            if (other.gameObject == tower.currentObjective.gameObject)
            {
                StopAllCoroutines();
                tower.Search();
            }
        }
    }

}
