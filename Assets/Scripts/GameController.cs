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
    float minionsAlive;
    public Vector3[] minionsTab;
    public float timeDay;
    int wave;
    public Text text;
    bool minionsSpawned;
    public GameObject SideAreaPrefab;
    public GameObject LoadingScreen;
    Vector3 SideAreaPosition;
    public GameObject player;
    float loadingScreenTime;
    public GameObject TeleportScreen;
    public Transform spawnPoint;
    void Start()
    {
        timeDay = 0;
        wave = 0;
        minionsSpawned = false;
        SideAreaPosition = new Vector3(500, 0, 500);
        ShipHP = 30;
    }

    // Update is called once per frame
    void Update()
    {

        text.text = ((int) timeDay).ToString();
        if (timeDay > 0 && minionsAlive<=0)
            timeDay -= Time.deltaTime;
        if (loadingScreenTime > 0)
        {
            loadingScreenTime -= Time.deltaTime;
        }
        else
        {
            LoadingScreen.SetActive(false);
        }
        if (timeDay <= 0 && wave<10 && !minionsSpawned)
        {
            Debug.Log(wave);
            Spawner1.SendMessage("NewEnemy", minionsTab[wave].x);
            Spawner2.SendMessage("NewEnemy", minionsTab[wave].y);
            Spawner3.SendMessage("NewEnemy", minionsTab[wave].z);
            minionsAlive = minionsTab[wave].x  + minionsTab[wave].y + minionsTab[wave].z;
            wave++;
            minionsSpawned = true;
            Debug.Log(minionsAlive);
        }
        if (minionsAlive <= 0 && minionsSpawned)
        {
            timeDay = 10;
            minionsSpawned = false;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(TeleportScreen.activeSelf)
                TeleportScreen.SetActive(false);
            else
                TeleportScreen.SetActive(true);
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
        Instantiate(area, SideAreaPosition, transform.rotation);
        player.transform.position = SideAreaPosition;
        TeleportScreen.SetActive(false);
        
    }
    public void Back()
    {
        LoadingScreen.SetActive(true);
        loadingScreenTime = 2;
        player.transform.position = spawnPoint.position;
        TeleportScreen.SetActive(false);
    }
}
