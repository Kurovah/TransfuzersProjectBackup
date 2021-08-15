using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyMelee;
    public int numberOfEnemies;
    float timer=0;
    // Update is called once per frame
    void Update()
    {
        if (numberOfEnemies > 0)
            timer += Time.deltaTime;
        if (timer > 1.0f && numberOfEnemies > 0)
        {
            SpawnEnemy();
            timer = 0;
        }

    }
    
    public void SpawnEnemy()
    {
        Instantiate(EnemyMelee, transform.position, transform.rotation);
        numberOfEnemies--;
    }

    public void NewEnemy(int enemies)
    {
        Debug.Log("NewEnemy" + enemies);
        numberOfEnemies = enemies;
    }
}
