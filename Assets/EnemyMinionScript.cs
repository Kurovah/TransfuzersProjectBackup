using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyMinionScript : MonoBehaviour
{
    private enum State
    {
        Tower,
        Ship,
        Nothing,
        Die,
    }
    public Animator animator;
    public CharacterController control;
    public Seeker seeker;
    public float speed;
    GameObject ship;
    GameObject target;
    Path path;
    public int currentWaypoint;
    public Vector3 direction;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private State state;
    GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        ship = GameObject.FindGameObjectWithTag("Ship");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        target = ship;
        animator.SetBool("Walk Forward", true);
        seeker.StartPath(transform.position, ship.transform.position, OnPathCompleted);
        state = State.Nothing;
        
    }

    private void FixedUpdate()
    {

        switch (state)
        {
            default:
            case State.Nothing:
                if (path != null)
                {
                    state = State.Ship;
                }
                break;
            case State.Ship:
                if (currentWaypoint >= path.vectorPath.Count)
                {
                    state = State.Die;
                    break;
                }
                EnemyMovement();
                break;
            case State.Tower:
                //Add interactions with towers.
                break;
            case State.Die:
                gameController.SendMessage("EnemyDied");
                Destroy(gameObject);
                break;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ship.tag)
        {
            state = State.Die;
        }
    }
    #region Functions
    //Enemy Moving towards function.
    public void EnemyMovement()
    {
        direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        control.Move(direction * speed);
        if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < 0.5f)
        {
            currentWaypoint++;
        }
    }
    public void OnPathCompleted(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
        else
        {
            Debug.Log(p.error);
        }
    }
    #endregion
}

