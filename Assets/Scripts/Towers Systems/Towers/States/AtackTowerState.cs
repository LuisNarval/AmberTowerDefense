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
        StartCoroutine(Aim());
    }
    public void DisHandle()
    {
        stateActive = false;
        StopAllCoroutines();
    }

    IEnumerator Aim()
    {
        float timeToLerp = 0.0f;

        Vector3 direction = tower.currentObjective.position - tower.weaponPivot.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);

        while (timeToLerp < 1)
        {
            yield return new WaitForEndOfFrame();
            tower.weaponPivot.transform.rotation = Quaternion.Lerp(tower.weaponPivot.transform.rotation, toRotation, timeToLerp);

            timeToLerp += Time.deltaTime * 2.0f;
        }
        
        StartCoroutine(Shoot());

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
            Debug.Log("Shoot");
            yield return new WaitForSeconds(tower.shootRate);
        }
    }




    public void OnTriggerExit(Collider other)
    {
        if (stateActive)
        {
            if (other.gameObject == tower.currentObjective.gameObject)
            {
                tower.Search();
            }
        }
    }

}
