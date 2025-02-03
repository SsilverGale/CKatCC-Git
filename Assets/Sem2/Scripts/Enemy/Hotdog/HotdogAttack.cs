using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotdogAttack : MonoBehaviour
{
    //vairable for to hold bdr
    HotdogDetectRange hdr;
    //rigid body for physics
    Rigidbody rb;
    //player transform var
    Transform player;
    //variable to hold bn
    HotdogNavi navi;
    //variable to allow the burger to attack once per detection
    bool oneAttack = false;
    //variable so burger jumps straight
    Transform playerTrans;
    //variable to check if wall is in between player and burger
    bool isWallBetween = false;

    Ray ray;

    //hotdog bullet variable
    public GameObject HotDogBullet;

    void Start()
    {
        //getting all components and player
        hdr = GetComponentInChildren<HotdogDetectRange>();
        navi = GetComponent<HotdogNavi>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").transform;
    }


    // attack function by getting player position and invoking an attack
    public void Attack()
    {
        Invoke("Shoot", 1.5f);
    }

    //jump motion
    public void Shoot()
    {
        if (!oneAttack && !isWallBetween)
        {
            //Debug.Log("Shot");
            InstantiateBullet();
            Invoke("ReEnableMovement", 2f);
        }

    }

    //puts burger off attack cooldown
    public void EnableAttack()
    {
        oneAttack = false;
    }

    //re enable navmesh movement after attacking
    public void ReEnableMovement()
    {
        //Debug.Log("player hit");
        navi.FinishAttack();
        Invoke("EnableAttack", 2f);
        hdr.EnableCollider();
    }

    public void InstantiateBullet()
    {
        Instantiate(HotDogBullet,transform.position,Quaternion.LookRotation(player.position));
    }
}
