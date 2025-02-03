using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HotdogNavi : MonoBehaviour
{
    RaycastHit hit;
    //navemesh agent component variable
    NavMeshAgent agent;

    //player variable
    public Player player;

    bool seePlayer = false;

    bool isAttacking = false;

    bool isNavmeshActive = true;

    bool isDefenceActive = false;

    Transform ObjectiveTransform;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        //connects navmesh component
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
        if (!isAttacking && !seePlayer && isNavmeshActive)
        {
            //movement towards player
            //Debug.Log("tracking");
            agent.SetDestination(player.GetComponentInParent<Transform>().position);
        }
        if (isDefenceActive)
        {
            agent.SetDestination(ObjectiveTransform.position);
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    }

    //stopping movment before jumping
    public void StopMotion()
    {
        //Debug.Log("Stopping Motion");
        isAttacking = true;
        isNavmeshActive = false;
        agent.SetDestination(transform.position);
        GetComponent<NavMeshAgent>().enabled = false;

    }

    //after attack is finished re enables nav mesh and burger attack goes off cooldown
    public void FinishAttack()
    {
        //Debug.Log("Attack Finished");
        isAttacking = false;
        isNavmeshActive = true;
        GetComponent<NavMeshAgent>().enabled = true;
    }

    public void DefenceObjectiveSet(bool condition)
    {
        //plan to enable/disable different attack ranges for different things when defence is up
        isDefenceActive = condition;
    }
}
