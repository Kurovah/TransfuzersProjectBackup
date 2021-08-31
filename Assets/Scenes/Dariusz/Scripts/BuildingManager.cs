using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEditor.PackageManager;
using UnityEngine.Diagnostics;

public class BuildingManager : MonoBehaviour
{
    public Transform towerIconArea;
    public GameObject towerIconPrefab;
    [SerializeField] private GameObject towerTest;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Camera TowerCam;
    int currentIndex = -1;
    
    private TowerHUD _hud;

    void Awake()
    {
        _hud = TowerHUD.Instance;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && SceneManagerBehaviour.GetTurretAmount(currentIndex) > 0)
        {
            Vector3 towerPosition = -Vector3.one;
            Ray ray = TowerCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                towerPosition = hit.point;
                TryPlaceTurret(towerPosition, currentIndex);
            }
            
        }
    }

    public void UpdateTowerIcons()
    {
        TurretDataList tdl = Resources.Load("TurretDataContainer") as TurretDataList;
        foreach (Transform item in towerIconArea)
        {
            Destroy(item.gameObject);
        }

        //create icons for all the craftable turrets
        foreach (var item in tdl.turretList)
        {
            var icon = Instantiate(towerIconPrefab, towerIconArea);
            TowerIcon ti = icon.GetComponent<TowerIcon>();
            ti.index = tdl.turretList.IndexOf(item);
            ti.amountText.text = SceneManagerBehaviour.GetTurretAmount(tdl.turretList.IndexOf(item)).ToString();
            ti.Button.onClick.AddListener(() => SetBuildIndex(tdl.turretList.IndexOf(item)));
        } 
    }

    public void SetBuildIndex(int index)
    {
        if(SceneManagerBehaviour.GetTurretAmount(index) > 0) { currentIndex = index; }
    }

    public void TryPlaceTurret(Vector3 position, int index)
    {
        Instantiate(SceneManagerBehaviour.GetTurretData(index).prefab, position, Quaternion.identity);
        SceneManagerBehaviour.RemoveTurret(index);
    }
}
