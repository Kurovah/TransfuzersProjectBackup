using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideAreaScript : MonoBehaviour
{
    public GameObject redOre;
    int randomX, randomZ;
    Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        rotation = new Quaternion(-90, 0, 0, 0);
        for(int i = 0; i < 10; i++)
        {
            randomX = Random.Range(20, 100);
            randomZ = Random.Range(20, 100);
            Instantiate(redOre, transform.position - new Vector3(randomX, -3, randomZ), rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
