using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornAttack : MonoBehaviour
{
    //vairable for to hold bdr
    PopcornDetectionRange pdr;
    //rigid body for physics
    Rigidbody rb;
    //player transform var
    Transform player;
    //variable to hold bn
    PopcornNav navi;
    //variable to allow the burger to attack once per detection
    bool oneAttack = false;
    //variable so burger jumps straight
    Transform playerTrans;
    //variable to check if wall is in between player and burger
    bool isWallBetween = false;

    Ray ray;

    //Popcorn explosion variable
    public GameObject popcornExplosion;

    void Start()
    {
        //getting all components and player
        pdr = GetComponentInChildren<PopcornDetectionRange>();
        navi = GetComponent<PopcornNav>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player").transform;
    }


    // attack function by getting player position and invoking an attack
    public void Attack()
    {
        Invoke("Explode", 1.5f);
    }

    //jump motion
    public void Explode()
    {
        if (!oneAttack && !isWallBetween)
        {
            //Debug.Log("Shot");
            Invoke("InstantiateExplosion", 1f);
            Destroy(this.gameObject,1f);
        }

    }

    public void InstantiateExplosion()
    {
        GameObject.FindGameObjectWithTag("WaveSpawn").GetComponent<WaveSpawn>().DecreaseEnemyCount();
        Instantiate(popcornExplosion, transform.position, Quaternion.identity);
    }
}
