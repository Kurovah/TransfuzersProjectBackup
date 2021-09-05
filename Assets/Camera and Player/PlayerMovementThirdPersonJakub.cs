using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementThirdPersonJakub : MonoBehaviour
{
    public CharacterController controller;
    public GameObject thirdPersonCamera;
    public GameObject redZone;
    public int redMaterianNumber;
    public float speed;
    //public float turnSmoothTime;
    float turnSmoothVelocity;
    public Animator anim;
    public Transform model;
    private void Start()
    {
        controller.isTrigger.Equals(true);
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        //move only when the game is not paused and not in building mode
        if (!SceneManagerBehaviour.isBuilding && !SceneManagerBehaviour.gamePaused)
        {
            if (direction.magnitude >= 0.1f)
            {
                anim.SetBool("Moving", true);
                //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + thirdPersonCamera.transform.eulerAngles.y;
                //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                //transform.rotation = Quaternion.Euler(0f, angle, 0f);

                //Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                Vector3 right = Vector3.Cross(Camera.main.transform.forward, Vector3.up);
                Vector3 forward = Vector3.Cross(Vector3.up, right);
                Vector3 moveDir = -right * horizontal + forward * vertical;
                //change model rotation
                model.rotation = Quaternion.LookRotation(moveDir.normalized);

                controller.Move(moveDir.normalized * speed * Time.deltaTime);

            }
            else
            {
                anim.SetBool("Moving", false);
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
