using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreScript : MonoBehaviour
{
    GameObject player;
    public float speed;
    public float distance;
    // Update is called once per frame
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.Rotate(Vector3.right*90f);
    }
    private void Update()
    {
        distance = (transform.position - player.transform.position).magnitude;
        if ((transform.position - player.transform.position).magnitude < 7)
        {
            transform.position -= (transform.position - player.transform.position ) * speed * Time.deltaTime;
        }
        if((transform.position - player.transform.position).magnitude < 3)
        {
            PlayerMovementThirdPersonJakub playerScript= player.GetComponent<PlayerMovementThirdPersonJakub>();
            playerScript.redMaterianNumber += 10;
            Destroy(gameObject);
        }
    }
}
