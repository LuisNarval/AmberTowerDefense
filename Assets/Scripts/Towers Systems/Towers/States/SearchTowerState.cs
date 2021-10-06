using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsable for managing the Search State of the towers.
/// It implements the ITowerState interface, following the State Design Pattern
/// This class execute a turn around animation for the upper torret using a coroutine.
/// Also seeks for enemies that exists in the perimeter and call the Atack State if finds someone.
/// </summary>

public class SearchTowerState : MonoBehaviour, ITowerState
{
    private Tower tower;
    bool stateActive = false;
    public void Handle(Tower _tower)
    {
        tower = _tower;
        stateActive = true;
        CheckArea();
        StartCoroutine(Turn());
    }
    public void DisHandle()
    {
        stateActive = false;
        StopAllCoroutines();
    }


    private void CheckArea()
    {
        float radius = tower.GetComponentInChildren<SphereCollider>().radius *
                 tower.GetComponentInChildren<SphereCollider>().gameObject.transform.localScale.x;

        Collider[] thingsInBounds = Physics.OverlapSphere(this.transform.position, radius);

        foreach(Collider thing in thingsInBounds){
            if (thing.CompareTag("Enemy"))
            {
                tower.Atack(thing.transform);
                break;
            }
        }
    }

    IEnumerator Turn()
    {
        float timeToLerp = 0.0f;

        while (timeToLerp < 1)
        {
            tower.weaponPivot.transform.rotation = Quaternion.Lerp(tower.weaponPivot.transform.rotation, Quaternion.identity, timeToLerp);
            timeToLerp += Time.deltaTime * 2.0f;
            yield return new WaitForEndOfFrame();
        }

        while (true)
        {
            tower.weaponPivot.Rotate(Vector3.up * Time.deltaTime * tower.turnSpeed);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (stateActive)
        {
            if (other.CompareTag("Enemy"))
            {
                tower.Atack(other.transform);
            }
        }
    }

}