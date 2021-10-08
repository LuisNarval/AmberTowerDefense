using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGhost : MonoBehaviour
{
    [SerializeField] private Material bodyMaterial;
    [SerializeField] private MeshRenderer zoneRenderer;
    
    [SerializeField] private Color rightColor = new Color(0 / 256, 255 / 256, 10 / 256, 65 / 256);
    [SerializeField] private Color wrongColor = new Color(255 / 256, 0 / 256, 27 / 256, 107 / 256);
    
    bool inGrid = false;

    public void SnapToGrid(Transform _grid)
    {
        if (!inGrid)
        {
            inGrid = true;
            Debug.Log("SET GRID");
            this.transform.position = _grid.position;
            zoneRenderer.enabled = true;
            bodyMaterial.color = rightColor;
        }
    }

    public void Follow(Vector3 _mousePosition){

        this.transform.position = _mousePosition;

        if (inGrid)
        {
            Debug.Log("UNSET");
            inGrid = false;
            zoneRenderer.enabled = false;
            bodyMaterial.color = wrongColor;
        }
    }

}