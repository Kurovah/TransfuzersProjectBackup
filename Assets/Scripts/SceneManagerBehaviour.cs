using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;

public class SceneManagerBehaviour : MonoBehaviour
{
    [Header("Areas")]
    public Transform pauseMenu;
    public Transform pauseTabAreas;
    public Transform inventorySlotArea;
    public Transform itemGetNotifArea;
    public Transform craftableTurretOptionArea;
    public Transform craftingComponentListingArea;
    public Transform buildingHUD;
    public Transform standardHUD;
    [Header("Prefabs")]
    public GameObject itemGetNotifPrefab;
    public GameObject inventoryDisplayPrefab;
    public GameObject craftableTurretOptionPrefab;
    public GameObject craftingComponentPrefab;
    [Header("Cameras")]
    public CinemachineVirtualCamera overHeadCam;
    public CinemachineFreeLook thirdPersonCam;
    //item index quantity
    public Dictionary<int, int> inventory;
    public ItemData itemList;
    public static ItemInventory items;
    public static TurretInventory turrets;

    public static bool isBuilding;
    public static bool gamePaused;

    //for the selections that happen withing a tab
    int subIndex = 0;

    
    
    // Start is called before the first frame update
    void Start()
    {
        //for the purpose of testing, these wiill be blank at first
        items = new ItemInventory();
        turrets = new TurretInventory();


        inventory = new Dictionary<int, int>();
        TogglePause(false);
        //UpdateInventoryUI();
    }

    // Update is called once per frame
    void Update()
    {
        //for testing item gain
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddItem(1);
        }

        //toggle buidling mode (make sure)
        if (Input.GetKeyDown(KeyCode.O) && !gamePaused)
        {
            isBuilding = !isBuilding;
            ToggleBuilding(isBuilding);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            gamePaused = !gamePaused;
            isBuilding = false;
            TogglePause(gamePaused);
            ToggleBuilding(isBuilding);
        }
    }

    public void ToggleBuilding(bool _isBuilding)
    {
        standardHUD.gameObject.SetActive(!_isBuilding);
        buildingHUD.gameObject.SetActive(_isBuilding);

        //changing cameras
        overHeadCam.Priority = Convert.ToInt32(_isBuilding);
        thirdPersonCam.Priority = Convert.ToInt32(!_isBuilding);

        FindObjectOfType<BuildingManager>().UpdateTowerIcons();
    }

    public void TogglePause(bool newPausedState)
    {
        pauseMenu.gameObject.SetActive(newPausedState);
        ChangeMenuTab(1);
        UpdateTurretSynthesisList();
        //changing cameras
    }

    #region add and remove items from inventory
    public void AddItem( int amount)
    {
        if (items.dictionary.ContainsKey(1))
        {
            items.dictionary[1] += amount;
        }
        else
        {
            items.dictionary.Add(1, amount);
        }
        InvenAddNotify(1);
    }

    public void RemoveItem(int index, int amount)
    {
        if (items.dictionary.ContainsKey(index))
        {
            items.dictionary[index] -= amount;
            if (items.dictionary[index] <= 0)
            {
                items.dictionary.Remove(index);
            }
        }
    }
    public static int getItemAmount(int index)
    {
        if (items.dictionary.ContainsKey(index))
        {
            return items.dictionary[index];
        }
        else
        {
            return 0;
        }
    }

    public static int GetTurretAmount(int index)
    {
        if (turrets.dictionary.ContainsKey(index))
        {
            return turrets.dictionary[index];
        }
        else
        {
            return 0;
        }
    }
    #endregion
    #region add and remove turrets from invertory
    public static void AddTurret(int index, int amount = 1)
    {
        if (turrets.dictionary.ContainsKey(index))
        {
            turrets.dictionary[index]++;
        } else
        {
            turrets.dictionary.Add(index,amount);
        }
    }
    public static void RemoveTurret(int index, int amount = 1)
    {
        if (turrets.dictionary.ContainsKey(index))
        {
            turrets.dictionary[index] -= amount;
            if (turrets.dictionary[index] <= 0)
            {
                turrets.dictionary.Remove(index);
            }
        }
    }
    #endregion
    public void ToggleInventory()
    {
        
    }

    public void InvenAddNotify(int index = 0)
    {
        //look for the item that is being obtained and create a little popup on the side
        ItemData item = (ItemData)Resources.Load("ItemDataContainer");
        var notif = Instantiate(itemGetNotifPrefab, itemGetNotifArea);
        notif.GetComponent<CollectionNotifBehaviour>().SetText(item.items[index].name);
    }

    public void ChangeMenuTab(int newTab)
    {
        foreach (Transform item in pauseTabAreas)
        {
            item.gameObject.SetActive(false);
        }
        pauseTabAreas.GetChild(newTab).gameObject.SetActive(true);
        subIndex = 0;
    }

    #region synthesis related
    #region for displaying the recipes and crafting components

    public void UpdateTurretSynthesisList()
    {
        TurretDataList recipelist = (TurretDataList)Resources.Load("TurretDataContainer");

        //refresh list
        foreach (Transform item in craftableTurretOptionArea)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in recipelist.turretList)
        {
            var entry = Instantiate(craftableTurretOptionPrefab, craftableTurretOptionArea);
            entry.GetComponent<Button>().onClick.AddListener(() => UpdateComponentList(entry.transform.GetSiblingIndex()));
            entry.GetComponentInChildren<Text>().text = item.turretName;
        }
        UpdateComponentList(0);
    }

    public void UpdateComponentList(int recipieIndex)
    {
        TurretDataList recipelist = (TurretDataList)Resources.Load("TurretDataContainer");
        ItemData itemList = (ItemData)Resources.Load("ItemDataContainer");
        TurretData r = recipelist.turretList[recipieIndex];

        //need to keeptrack of the index;
        subIndex = recipieIndex;

        //change color of clicked button by resetting all the button and changing the color of the selected one
        foreach (Transform item in craftableTurretOptionArea)
        {
            item.gameObject.GetComponent<Image>().color = ColorPallette.colors[3];
            item.gameObject.GetComponentInChildren<Text>().color = ColorPallette.colors[0];
        }

        craftableTurretOptionArea.GetChild(recipieIndex).gameObject.GetComponent<Image>().DOColor(ColorPallette.colors[0], .2f);
        craftableTurretOptionArea.GetChild(recipieIndex).GetComponentInChildren<Text>().DOColor(ColorPallette.colors[2], .2f);

        //refresh th component list
        foreach (Transform item in craftingComponentListingArea)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in r.components)
        {
            var entry = Instantiate(craftingComponentPrefab, craftingComponentListingArea);
            //set the name of the  component
            entry.transform.GetChild(0).GetComponent<Text>().text = itemList.items[item.itemIndex].name;
            entry.transform.GetChild(1).GetComponent<Text>().text = item.itemAmount.ToString();
            entry.transform.GetChild(2).GetComponent<Text>().text = getItemAmount(item.itemIndex).ToString();
            //change colour of text depending on wether yoy have enough mats
            entry.transform.GetChild(2).GetComponent<Text>().color = getItemAmount(item.itemIndex) >= item.itemAmount ? ColorPallette.colors[0] : ColorPallette.colors[4];
        }

        
    }
    #endregion

    public void TrySynthesize()
    {
        TurretDataList recipelist = (TurretDataList)Resources.Load("TurretDataContainer");
        TurretData r = recipelist.turretList[subIndex];
        bool craftable = true;
        //check if you have enough mats
        foreach (var item in r.components)
        {
            if (item.itemAmount > getItemAmount(item.itemIndex))
                craftable = false;
        }

        //remove if you do skip if you don't
        if (craftable)
        {
            foreach (var item in r.components)
            {
                RemoveItem(item.itemIndex, item.itemAmount);
                Debug.Log(getItemAmount(item.itemIndex));
            }

            AddTurret(subIndex);

            UpdateComponentList(subIndex);
        }
        
    }
    #endregion
    public static TurretData GetTurretData(int index)
    {
        TurretDataList tdl = Resources.Load("TurretDataContainer") as TurretDataList;
        return tdl.turretList[index];
    }
}
