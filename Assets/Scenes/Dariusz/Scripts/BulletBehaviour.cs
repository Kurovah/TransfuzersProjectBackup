using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject target;
    public GameObject impactEffect;
    public float speed = 70.0f;
    EnemyMinionScript targetScript;


    public void Seek(GameObject target)
    {
       this.target = target;
        targetScript = this.target.GetComponent<EnemyMinionScript>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
        
        if (target == null)
        {
            Destroy(gameObject);

            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2.0f);
        targetScript.hp -= 25;
        Destroy(gameObject);
    }
}
