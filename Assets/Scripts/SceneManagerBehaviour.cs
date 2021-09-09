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
    public Transform inventorySlotArea;
    public Transform itemGetNotifArea;
    public Transform buildingHUD;
    public Transform standardHUD;
    [Header("Prefabs")]
    public GameObject itemGetNotifPrefab;
    public GameObject inventoryDisplayPrefab;

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

    //static refs
    static GameObject itemGetNotifPrefabStaticRef;
    static Transform notifDisplayStaticRef;

    //for the selections that happen withing a tab
    public PauseMenuBehaviour pauseMenuBehaviour;
    
    
    // Start is called before the first frame update
    void Start()
    {
        itemGetNotifPrefabStaticRef = itemGetNotifPrefab;
        notifDisplayStaticRef = itemGetNotifArea;

        //for the purpose of testing, these wiill be blank at first
        items = new ItemInventory();
        turrets = new TurretInventory();

        inventory = new Dictionary<int, int>();
        TogglePause(false);

        //get pause menu so that you can pause it
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
        pauseMenuBehaviour.ChangeMenuTab(0);
        pauseMenuBehaviour.UpdateTab();
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

    public static void RemoveItem(int index, int amount)
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
    public static void InvenAddNotify(int index = 0)
    {
        //look for the item that is being obtained and create a little popup on the side
        ItemData item = (ItemData)Resources.Load("ItemDataContainer");
        var notif = Instantiate(itemGetNotifPrefabStaticRef, notifDisplayStaticRef);
        notif.GetComponent<CollectionNotifBehaviour>().SetText(item.items[index].name);
    }

   
    public static TurretData GetTurretData(int index)
    {
        TurretDataList tdl = Resources.Load("TurretDataContainer") as TurretDataList;
        return tdl.turretList[index];
    }
}
