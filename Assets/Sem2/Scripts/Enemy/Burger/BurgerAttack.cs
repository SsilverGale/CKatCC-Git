using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerAttack : MonoBehaviour
{
    //vairable for to hold bdr
    BurgerDetectRange bdr;
    //rigid body for physics
    Rigidbody rb;
    //player transform var
    Transform player;
    //variable to hold bn
    BurgerNavi navi;
    //variable to allow the burger to attack once per detection
    bool oneAttack = false;
    //variable so burger jumps straight
    Transform playerTrans;
    //variable to check if wall is in between player and burger
    bool isWallBetween = false;

    void Start()
    {
        //getting all components and player
        bdr = GetComponentInChildren<BurgerDetectRange>();
        navi = GetComponent<BurgerNavi>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").transform;
    }


    // attack function by getting player position and invoking an attack
    public void Attack()
    {
        
        playerTrans = player.transform;
        Invoke("Jump",1.5f);
    }

    //jump motion
    public void Jump()
    {
        if (!oneAttack && !isWallBetween) 
        {
            //Debug.Log("Jumped");
            rb.AddForce((playerTrans.transform.position - transform.position) * 2 + new Vector3(0,2,0), ForceMode.Impulse);
            oneAttack = true;
            Invoke("ReEnableMovement", 2f);
        }
        
    }

    //collisions to detect when the burger hits player
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {

            Invoke("zeroVelocity", 0.7f);
            Invoke("ReEnableMovement", 2f);
        }
    }

    //puts burger off attack cooldown
    public void EnableAttack()
    {
        oneAttack = false;
    }

    void zeroVelocity()
    {
        rb.velocity = Vector3.zero;
    }

    //re enable navmesh movement after attacking
    public void ReEnableMovement()
    {
        //Debug.Log("player hit");
        navi.FinishAttack();
        Invoke("EnableAttack", 2f);
        bdr.EnableCollider();
    }
}
