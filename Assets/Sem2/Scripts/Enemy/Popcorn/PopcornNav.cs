using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PopcornNav : MonoBehaviour
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

    PopcornAnimations PA;

    void Start()
    {
        PA = GetComponent<PopcornAnimations>();
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
            agent.SetDestination(player.GetComponentInParent<Transform>().position);
            PA.PlayWalk();
        }
        if (isDefenceActive)
        {
            agent.SetDestination(ObjectiveTransform.position);
            PA.PlayWalk();
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    }

    //stopping movment before jumping
    public void StopMotion()
    {
        PA.StopWalk();
        //Debug.Log("Stopping Motion");
        isAttacking = true;
        isNavmeshActive = false;
        agent.SetDestination(transform.position);

    }

    //after attack is finished re enables nav mesh and burger attack goes off cooldown
    public void FinishAttack()
    {
        Debug.Log("Attack Finished");
        isNavmeshActive = true;
        isAttacking = false;
        PA.PlayWalk();
    }

    public void DefenceObjectiveSet(bool condition)
    {
        //plan to enable/disable different attack ranges for different things when defence is up
        isDefenceActive = condition;
    }
}
