using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickabelScript : MonoBehaviour
{
    public GameObject player;
    public GameObject pickUp;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if((player.transform.position - transform.position).magnitude <= 10)
        {
            pickUp.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Picked up");
                
            }
        }
        
    }
}
