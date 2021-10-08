using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The placement System is gonna detect the input from the user and will let him put a Tower in the field.
/// First it's gonna show a Ghost, and when the user clics on a grid, the Placement System will call to the Towers Pools
/// so a new Tower is pull from the pool & it´s placed in the grid.
/// </summary>

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] TowerConfiguration towerConfiguration;
    [SerializeField] Transform buyPanel;
    [SerializeField] int[] Prices;
    [SerializeField] Image[] buttons; 
    [SerializeField] TowerGhost[] ghost;


    int towerSelected = 0;
    bool follow;

    private TowerSpawnSystem towerSpawnSystem;

    private void Start()
    {
        Invoke("CreatePool", 2.0f);
    }

    void CreatePool()
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
        if (ServiceLocator.GetService<Bank>().CanRetire(Prices[_option]))
        {
            ghost[towerSelected].gameObject.SetActive(false);
            towerSelected = _option;
            ghost[towerSelected].gameObject.SetActive(true);
            follow = true;
            buttons[_option].color = Color.green;
        }
        else
        {
            buttons[_option].color = Color.red;
            StartCoroutine(ShakePanel());
        }

    }
    
    void UnselectOption()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].color = Color.white;
        }
        ghost[towerSelected].gameObject.SetActive(false);
        follow = false;
    }

    void SetTower(Collider _towerGrid)
    {
        UnselectOption();
        _towerGrid.enabled = false;

        if (ServiceLocator.GetService<Bank>().CanRetire(Prices[towerSelected]))
        {
            towerSpawnSystem.SpawnTower(towerConfiguration.towers[towerSelected].ID, _towerGrid.transform.position);
            ServiceLocator.GetService<Bank>().RetireMoney(Prices[towerSelected]);
        }
    }
 
    
    IEnumerator ShakePanel()
    {
        float time = 0;
        float xMove;
        while (time < 1)
        {
            xMove = Mathf.Sin(time*25)*2.5f;
            buyPanel.transform.Translate(Vector3.right*xMove);    
            time += Time.deltaTime*2.0f;
            yield return new WaitForEndOfFrame();
        }
        buyPanel.transform.position = Vector3.zero;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].color = Color.white;
        }
    }

}