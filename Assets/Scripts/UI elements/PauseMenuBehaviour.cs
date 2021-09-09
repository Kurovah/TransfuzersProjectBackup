using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PauseMenuBehaviour : MonoBehaviour
{
    
    public int currentTab = 0;
    int subIndex = 0;
    public Transform pauseTabAreas;
    public Transform SynThesisScrollList;
    public Transform DataLogScrollList;
    public Transform TabButtons;
    public Transform craftingComponentListingArea;
    public GameObject MainListOptionPrefab;
    public GameObject craftingComponentPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTab()
    {
        switch(currentTab){
            case 0://map
                 
                break;
            case 1:

                break;

            case 2:

                break;
            case 3://synthesis
                UpdateTurretSynthesisList();
                break;

            case 4://DataLogs
                UpdateDataLogList();
                break;

            case 5://options

                break;
        }
    }

    public void ChangeMenuTab(int newTab)
    {
        foreach (Transform item in pauseTabAreas)
        {
            item.gameObject.SetActive(false);
        }
        pauseTabAreas.GetChild(newTab).gameObject.SetActive(true);

        //change color of button
        subIndex = 0;
        currentTab = newTab;
        UpdateTabHead(newTab);
        UpdateTab();
        
    }

    

    #region synthesis related
    #region for displaying the recipes and crafting components

    public void UpdateTurretSynthesisList()
    {
        TurretDataList recipelist = (TurretDataList)Resources.Load("TurretDataContainer");

        //refresh list
        foreach (Transform item in SynThesisScrollList)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in recipelist.turretList)
        {
            var entry = Instantiate(MainListOptionPrefab, SynThesisScrollList);
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
        int i = 0;
        //change color of clicked button by resetting all the button and changing the color of the selected one
        foreach (Transform item in SynThesisScrollList)
        {
            item.gameObject.GetComponent<Image>().color = ColorPallette.colors[3];
            item.gameObject.GetComponentInChildren<Text>().color = ColorPallette.colors[0];

            if(i == recipieIndex)
            {
                item.GetChild(0).DOPunchPosition(Vector3.down, 0.5f, 0);
                item.gameObject.GetComponent<Image>().DOColor(ColorPallette.colors[0], .2f);
                item.gameObject.GetComponentInChildren<Text>().DOColor(ColorPallette.colors[2], .2f);
            }

            i++;
        }

        

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
            entry.transform.GetChild(2).GetComponent<Text>().text = SceneManagerBehaviour.getItemAmount(item.itemIndex).ToString();
            //change colour of text depending on wether yoy have enough mats
            entry.transform.GetChild(2).GetComponent<Text>().color = SceneManagerBehaviour.getItemAmount(item.itemIndex) >= item.itemAmount ? ColorPallette.colors[0] : ColorPallette.colors[4];
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
            if (item.itemAmount > SceneManagerBehaviour.getItemAmount(item.itemIndex))
                craftable = false;
        }

        //remove if you do skip if you don't
        if (craftable)
        {
            foreach (var item in r.components)
            {
                SceneManagerBehaviour.RemoveItem(item.itemIndex, item.itemAmount);
                Debug.Log(SceneManagerBehaviour.getItemAmount(item.itemIndex));
            }

            SceneManagerBehaviour.AddTurret(subIndex);

            UpdateComponentList(subIndex);
        }

    }
    #endregion

    #region datalog related
    public void UpdateDataLogList()
    {

    }
    public void UpdateDataLogContent(int enemyIndex)
    {

    }
    #endregion

    public void UpdateTabHead(int newTab)
    {
        int i = 0;
        foreach (Transform item in TabButtons)
        {
            //change the color of the text and background
            item.gameObject.GetComponent<Image>().color = ColorPallette.colors[3];
            item.gameObject.GetComponentInChildren<Text>().color = ColorPallette.colors[0];

            if(i == newTab)
            {
                item.GetChild(0).DOPunchPosition(Vector3.down, 0.5f,0);
                item.gameObject.GetComponent<Image>().color = ColorPallette.colors[0];
                item.gameObject.GetComponentInChildren<Text>().color = ColorPallette.colors[3];
            }
                
            i++;
        }

        
    }
}
