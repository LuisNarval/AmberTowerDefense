using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The placement System is gonna detect the input from the user and will let him put a Tower in the field.
/// First it's gonna show a Ghost, and when the user clics on a grid, the Placement System will call to the Towers Pools. 
/// </summary>

public class PlacementSystem : MonoBehaviour
{

    [SerializeField] private LayerMask layer;

    [SerializeField] GameObject ghost1;
    [SerializeField] GameObject ghost2;
    [SerializeField] GameObject ghost3;

    private GameObject selectedGhost;

    private void Awake()
    {
        selectedGhost = ghost1;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, layer))
            {
                Debug.Log ("Hit a : " + hit.collider.gameObject.name);
            }
        }
    }


    public void SelectTower1()
    {
        ShowGhost(ghost1);
    }

    public void SelectTower2()
    {
        ShowGhost(ghost2);
    }

    public void SelectTower3()
    {
        ShowGhost(ghost3);
    }

    void ShowGhost(GameObject _ghost)
    {
        selectedGhost.SetActive(false);
        selectedGhost = _ghost;
        selectedGhost.SetActive(true);
        //StartCoroutine("FollowMouse");
    }




    IEnumerator FollowMouse()
    {
        selectedGhost.SetActive(true);

        Camera cam = Camera.main;
        while (true)
        {

            /*Vector3 mp = Input.mousePosition;
            Vector3 wp = cam.ScreenToWorldPoint(new Vector3(mp.x, mp.y, cam.nearClipPlane));

            selectedGhost.transform.position = wp;
            Debug.Log("Mouse Position: " + mp);
            Debug.Log("Wolrd Position: " + wp);*/

            



            yield return new WaitForEndOfFrame();
        }
    }



}