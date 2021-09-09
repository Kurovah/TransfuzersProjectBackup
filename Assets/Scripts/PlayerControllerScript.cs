using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public float moveSpeed;
    Vector3 moveDir;
    CharacterController character;
    public Transform playerModel;
    bool canMove;
    public Animator animator;
    List<Collider> hasHit = new List<Collider>();
    enum states
    {
        normal,
        attack,
        dead
    }
    states currentState;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        currentState = states.normal;
    }

    // Update is called once per frame
    void Update()
    { 

        switch (currentState)
        {
            //moving and idling
            case states.normal:
                Vector3 rightVec = Camera.main.transform.right;
                Vector3 forwardVec = Vector3.Cross(rightVec, Vector3.up);
                moveDir = rightVec * Input.GetAxis("Horizontal") + forwardVec * Input.GetAxis("Vertical");
                moveDir = moveDir.normalized;
                animator.SetFloat("MoveSpeed", moveDir.magnitude);
                character.Move(moveDir * moveSpeed * Time.deltaTime);

                if (moveDir.magnitude > 0.1f)
                {

                    playerModel.rotation = Quaternion.Slerp(playerModel.rotation, Quaternion.LookRotation(moveDir), 0.1f);
                }

                if (Input.GetKey(KeyCode.H))
                {
                    animator.SetTrigger("Attack");
                    currentState = states.attack;

                }
                break;
            //attacking
            case states.attack:
                animator.ResetTrigger("Attack");
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .9f)
                {
                    hasHit.Clear();
                    currentState = states.normal;
                }

                if (HelperFunctions.IsBetween(0.08f, 0.16f, animator.GetCurrentAnimatorStateInfo(0).normalizedTime))
                {
                    CheckHit();
                }
                break;
        }
    }

    public void CheckHit()
    {
        Collider[] hits = Physics.OverlapSphere(playerModel.position + playerModel.forward, 2, 9);
        if (hits.Length > 0)
        {
            Debug.Log("hits" + hits.Length);
            foreach (Collider collider in hits)
            {
                var a = collider.gameObject.GetComponent<ResourcePointBehaviour>();
                if (a == null || hasHit.Contains(collider))
                { Debug.Log("no res"); continue; }

                a.DropItem();
                hasHit.Add(collider);
            }
        }
    }
}
