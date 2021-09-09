using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject Spawner1;
    public GameObject Spawner2;
    public GameObject Spawner3;
    public int ShipHP;
    public float minionsAlive;
    public Vector3[] minionsTab;
    public float timeDay;
    public int wave;
    public bool minionsSpawned;
    public GameObject SideAreaPrefab;
    public GameObject LoadingScreen;
    Vector3 SideAreaPosition;
    public GameObject player;
    float loadingScreenTime;
    public GameObject TeleportScreen;
    public Transform spawnPoint;
    public Text DayCount;
    public GameObject pickableController;
    GameObject side;
    GameObject pickCont;
    void Start()
    {
        timeDay = 0;
        wave = 1;
        minionsSpawned = false;
        SideAreaPosition = new Vector3(1500, 0, 1500);
        ShipHP = 30;
    }

    // Update is called once per frame
    void Update()
    {
        DayCount.text = wave.ToString();
        if (timeDay < 60 && minionsAlive<=0)
            timeDay += Time.deltaTime;
        if (loadingScreenTime > 0)
        {
            loadingScreenTime -= Time.deltaTime;
        }
        else
        {
            LoadingScreen.SetActive(false);
        }
        if (timeDay >= 60 && wave<10 && !minionsSpawned)
        {
            Debug.Log(wave);
            Spawner1.SendMessage("NewEnemy", minionsTab[wave].x);
            Spawner2.SendMessage("NewEnemy", minionsTab[wave].y);
            Spawner3.SendMessage("NewEnemy", minionsTab[wave].z);
            minionsAlive = minionsTab[wave].x  + minionsTab[wave].y + minionsTab[wave].z;
            
            minionsSpawned = true;
            Debug.Log(minionsAlive);
        }
        if (minionsAlive <= 0 && minionsSpawned)
        {
            timeDay = 0;
            minionsSpawned = false;
            wave++;
        }
        if (Input.GetKeyDown(KeyCode.Tab) )
        {
            if(TeleportScreen.activeSelf)
                TeleportScreen.SetActive(false);
            else
                if(timeDay< 60)
                    TeleportScreen.SetActive(true);
        }
        if(timeDay>= 60 && player.transform.position.magnitude >1000)
        {
            player.transform.position = spawnPoint.transform.position;
        }
    }
    public void EnemyDied()
    {
        minionsAlive--;
        
    }
    public void Teleport(GameObject area)
    {
        LoadingScreen.SetActive(true);
        loadingScreenTime = 2;
        side = Instantiate(area, SideAreaPosition, transform.rotation);
        pickCont = Instantiate(pickableController, SideAreaPosition+new Vector3(0,2,0), transform.rotation);
        player.transform.position = SideAreaPosition + new Vector3(0, 2, 0);
        TeleportScreen.SetActive(false);
        
    }
    public void Back()
    {
        Destroy(side);
        Destroy(pickCont);
        LoadingScreen.SetActive(true);
        loadingScreenTime = 2;
        player.transform.position = spawnPoint.position;
        TeleportScreen.SetActive(false);
    }
}
