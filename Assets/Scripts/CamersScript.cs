using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamersScript : MonoBehaviour
{
    public Transform target, cam;
    public float zoom, cameraRotationSpeed, smoothing;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, 1), cameraRotationSpeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, 1), -cameraRotationSpeed);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, smoothing);
        cam.position = transform.position - (transform.forward - transform.up).normalized * zoom;
        //cam.LookAt(target);

    }
}
