using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BurgerNavi : MonoBehaviour
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


    BurgerAnimations BA;
    void Start()
    {
        BA = GetComponent<BurgerAnimations>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        //connects navmesh component
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isAttacking) 
        {
            transform.LookAt(player.transform.position);
        }
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
            if (!isAttacking && !seePlayer && isNavmeshActive)
        {
            BA.PlayWalk();
            //movement towards player
            agent.SetDestination(player.GetComponentInParent<Transform>().position);
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    }

    //stopping movment before jumping
    public void StopMotion()
    {
        BA.JumpWind(true);
        BA.StopWalk();
        isAttacking = true;
        agent.SetDestination(transform.position);
        isNavmeshActive = false;
        GetComponent<NavMeshAgent>().enabled = false;

    }

    //after attack is finished re enables nav mesh and burger attack goes off cooldown
    public void FinishAttack()
    {
        BA.PlayWalk();
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
