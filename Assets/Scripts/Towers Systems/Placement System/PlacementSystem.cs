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
    [SerializeField] private CanvasGroup group;

    [SerializeField] GameObject[] ghost;


    int towerSelected = 0;
    Transform towerBase;

    private void Awake()
    {
        group.alpha = 0;
        group.blocksRaycasts = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, layer))
            {
                Debug.Log("TAG: " + hit.collider.tag);
                if (hit.collider.CompareTag("TowerGrid"))
                {
                    if (towerBase != null)
                        towerBase.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);

                    towerBase = hit.transform;
                    towerBase.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);
                    ShowMenu();
                }

            }
            else
            {
                if (group.alpha > 0)
                {
                    //towerBase.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.white);
                    CloseMenu();
                }
            }
        }
    }


    public void SelectOpcion(int _option)
    {
        ghost[towerSelected].SetActive(false);
        towerSelected = _option;
        ghost[towerSelected].transform.position = towerBase.position;
        ghost[towerSelected].SetActive(true);
    }
    
    public void CloseMenu()
    {
        StartCoroutine(FadeOut(group));
    }

    public void ShowMenu()
    {
        StartCoroutine(FadeIn(group));
    }

    public void Buy()
    {

    }



    IEnumerator FadeIn(CanvasGroup _group)
    {
        _group.blocksRaycasts = false;
        while (_group.alpha < 1)
        {
            _group.alpha += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _group.blocksRaycasts = true;
    }

    IEnumerator FadeOut(CanvasGroup _group)
    {
        _group.blocksRaycasts = false;
        while (_group.alpha > 0)
        {
            _group.alpha -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}