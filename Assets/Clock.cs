using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public GameController controller;
    public float rotation;
    public Vector3 finalRotation;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (controller.timeDay <= 90)
        {
            rotation = 90 - controller.timeDay*3;
            if (rotation < 0)
            {
                rotation += 360;
            }


            transform.eulerAngles = new Vector3(0, 0, rotation);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
    }
}
