using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SocialPlatforms;

public class TowerBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject target;
    [SerializeField] private Transform tower;
    [SerializeField] private float fireCountdown = 0f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public string enemyTag = "Enemy";
    public float range = 15.0f;
    public float fireRate = 1.0f;
    public float rotationSpeed = 10.0f;
   
    // Start is called before the first frame update
    void Start()
    {
        tower = gameObject.transform;
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(tower.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        tower.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);

        if (fireCountdown <= 0.0f)
        {
            Shoot();
            fireCountdown = 1.0f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletBehaviour bullet = bulletGO.GetComponent<BulletBehaviour>();

        if (bullet != null)
            bullet.Seek(target);
    }
    
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy;
        }
        else
        {
            target = null;
        }
    }
}
