using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldEnemyBehaviour : MonoBehaviour, IHurtable
{
    NavMeshAgent agent;
    Coroutine attackCoroutine;
    public float dectectionRadius, moveSpeed;
    PlayerControllerScript player;
    Vector3 homePos;
    Mesh drawMesh;

    enum EnemyStates : int
    {
        wander,
        alert,
        attack,
        dead
    }

    EnemyStates state = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        player = FindObjectOfType<PlayerControllerScript>();
        homePos = transform.position;
        state = EnemyStates.wander;

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyStates.wander:
                WanderState();
                break;
            case EnemyStates.alert:
                AlertState();
                break;
            case EnemyStates.attack:
                break;
            case EnemyStates.dead:
                break;
        }

    }

    void WanderState()
    {
        if(Vector3.Distance(player.transform.position, transform.position) < dectectionRadius) 
        {
            state = EnemyStates.alert;
        }
    }

    void AlertState()
    {
        agent.destination = player.transform.position;
    }

    void AttackState()
    {
        if(attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(Attack());
        }
    }

    

    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.DrawWireDisc(transform.position, transform.up, dectectionRadius);
    }

    IEnumerator Attack()
    {
        yield return null;
    }

    void CheckPlayerHit()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position + transform.forward, 1f);
        foreach (var item in hits)
        {
            IHurtable i = item.gameObject.GetComponent<IHurtable>();
            if (i == null || i == this) { continue; }
            i.OnHurt();
        }
    }

    public void OnHurt()
    {
        throw new System.NotImplementedException();
    }

    public void OnDeath()
    {
        throw new System.NotImplementedException();
    }
    private void OnDrawGizmos()
    {
        var navMesh = NavMesh.CalculateTriangulation();
        Vector3[] vertices = navMesh.vertices;
        int[] polygons = navMesh.indices;

        drawMesh = new Mesh();
        drawMesh.vertices = vertices;
        drawMesh.triangles = polygons;
        Gizmos.DrawMesh(drawMesh);
    }
}
