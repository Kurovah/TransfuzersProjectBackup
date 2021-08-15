using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreScript : MonoBehaviour
{
    GameObject player;
    public float speed;
    public float distance;
    public Rigidbody rb;
    // Update is called once per frame
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.Rotate(Vector3.right*90f);
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        distance = (transform.position - player.transform.position).magnitude;
        if ((transform.position - player.transform.position).magnitude < 15)
        {
            rb.AddForce((player.transform.position - transform.position ) * speed);
        }
        if((transform.position - player.transform.position).magnitude < 5)
        {
            PlayerMovementThirdPersonJakub playerScript= player.GetComponent<PlayerMovementThirdPersonJakub>();
            playerScript.redMaterianNumber += 10;
            Destroy(gameObject);
        }
    }
}
