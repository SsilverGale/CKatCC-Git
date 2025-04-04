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

    HotdogAnimations HA;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        //connects navmesh component
        agent = GetComponent<NavMeshAgent>();
        HA = GetComponent<HotdogAnimations>();
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
            HA.PlayWalk();
        }
        if (isDefenceActive)
        {
            agent.SetDestination(ObjectiveTransform.position);
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        if (isAttacking)
        {
            transform.LookAt(player.transform.position);
        }
    }

    //stopping movment before jumping
    public void StopMotion()
    {
        HA.StopWalk();
        HA.Attack();
        //Debug.Log("Stopping Motion");
        isAttacking = true;
        isNavmeshActive = false;
        agent.SetDestination(transform.position);
        GetComponent<NavMeshAgent>().enabled = false;
        transform.position += new Vector3(0, 0.5f, 0);

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BearTrap")
        {
            StopMotion();
            Invoke("FinishAttack", 2f);
        }
    }

    //after attack is finished re enables nav mesh and burger attack goes off cooldown
    public void FinishAttack()
    {
        //Debug.Log("Attack Finished");
        HA.PlayWalk();
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
