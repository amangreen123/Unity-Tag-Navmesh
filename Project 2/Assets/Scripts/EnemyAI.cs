using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;  //stores reference to the NavMesh Agent component
    public Transform player;
    public LayerMask whatIsGround,whatisPlayer;
    public float rayDist;

    bool walkPointSet;
    public float walkPointRange;
    public Vector3 walkPoint;
    public float timeBetweenAttacks;
    bool alreadyTagged;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInTagRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        playerInTagRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);

        if (!playerInSightRange && !playerInTagRange) Patroling();
        if (!playerInSightRange && !playerInTagRange) ChasePlayer();
        if (!playerInTagRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //if reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }


    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);


        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }


    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }


    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        if (!alreadyTagged)
        {
            Vector3 rayStart = transform.position + new Vector3(0, 0.5f, 0);
            Vector3 rayDir = transform.forward;
            RaycastHit hit;
            Physics.Raycast(rayStart, rayDir, out hit, rayDist);

            if (hit.collider != null)
            {
                UnityEngine.Debug.DrawLine(transform.position, hit.point, Color.red);
                if (hit.collider.gameObject.tag == "Player")
                {
                    Movement player = hit.collider.gameObject.GetComponent<Movement>();
                    alreadyTagged = true;
                    gameObject.GetComponent<Renderer>().material.color = Color.red;

                }
                Invoke(nameof(ResetAttack), timeBetweenAttacks);

            }
            
          
        }
    }

    private void ResetAttack()
    {
        alreadyTagged = false;
    }

}
