using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementThirdPersonJakub : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public GameObject thirdCamera;
    public GameObject isoCamera;
    public GameObject redZone;
    public int redMaterianNumber;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public Animator anim;
    private void Start()
    {
        thirdCamera.SetActive(false);
        isoCamera.SetActive(true);
        controller.isTrigger.Equals(true);
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        if (thirdCamera.activeSelf)
        {
            if (direction.magnitude >= 0.1f)
            {
                anim.SetBool("Moving", true);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                /*if (controller.isGrounded)
                {
                    controller.Move(moveDir.normalized * speed * Time.deltaTime);
                }
                else
                {
                    controller.SimpleMove(moveDir.normalized * speed * Time.deltaTime);
                }*/
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
                
            }
            else
            {
                anim.SetBool("Moving", false);
            }
            
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            if (thirdCamera.activeSelf == true)
            {
                thirdCamera.SetActive(false);
                isoCamera.SetActive(true);
            }
            else
            {
                thirdCamera.SetActive(true);
                isoCamera.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            transform.position = new Vector3(-200.0f, 2.0f, 0.0f);
        }
    }
    void AddRedMatterial()
    {
        redMaterianNumber += 1;
    }
}
