using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject Spawner1;
    public GameObject Spawner2;
    public GameObject Spawner3;
    float minionsAlive;
    public Vector3[] minionsTab;
    public float timeDay;
    int wave;
    public Text text;
    bool minionsSpawned;

    void Start()
    {
        timeDay = 0;
        wave = 0;
        minionsSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ((int) timeDay).ToString();
        if (timeDay > 0 && minionsAlive<=0)
            timeDay -= Time.deltaTime;
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
            timeDay = 3;
            minionsSpawned = false;
        }
    }
    public void EnemyDied()
    {
        minionsAlive--;
    }
}
