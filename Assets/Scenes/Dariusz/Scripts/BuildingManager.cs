using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEditor.PackageManager;
using UnityEngine.Diagnostics;

public class BuildingManager : MonoBehaviour
{
    
    [SerializeField] private GameObject towerTest;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Camera mainCamera;
    
    private TowerHUD _hud;

    void Awake()
    {
        _hud = TowerHUD.Instance;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && mainCamera.isActiveAndEnabled)
        {
            Vector3 towerPosition = -Vector3.one;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                towerPosition = hit.point;
            }
            Instantiate(towerTest, towerPosition, Quaternion.identity);
        }
    }
}
