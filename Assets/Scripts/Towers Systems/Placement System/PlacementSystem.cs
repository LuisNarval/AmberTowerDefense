using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The placement System is gonna detect the input from the user and will let him put a Tower in the field.
/// First it's gonna show a Ghost, and when the user clics on a grid, the Placement System will call to the Towers Pools. 
/// </summary>

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] TowerConfiguration towerConfiguration;
    [SerializeField] private CanvasGroup group;
    [SerializeField] TowerGhost[] ghost;

    int towerSelected = 0;
    bool follow;

    private TowerSpawnSystem towerSpawnSystem;

    private void Awake()
    {
        towerSpawnSystem = new TowerSpawnSystem(towerConfiguration, 5);    
    }

    private void Update()
    {
        if(follow)
            NewRaycast();
    }

    void NewRaycast()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("TowerGrid"))
            {
                ghost[towerSelected].SnapToGrid(hit.collider.transform);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    SetTower(hit.collider);
            }
            else
            {
                ghost[towerSelected].Follow(hit.point);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    UnselectOption();
            }
        }

    }

    public void SelectOpcion(int _option)
    {
        ghost[towerSelected].gameObject.SetActive(false);
        towerSelected = _option;
        ghost[towerSelected].gameObject.SetActive(true);
        follow = true;
    }
    
    void UnselectOption()
    {
        ghost[towerSelected].gameObject.SetActive(false);
        follow = false;
    }

    void SetTower(Collider _towerGrid)
    {
        UnselectOption();
        _towerGrid.enabled = false;
        
        towerSpawnSystem.SpawnTower(towerConfiguration.towers[towerSelected].ID, _towerGrid.transform.position);
    }
   
}