using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerMove : NetworkBehaviour
{
    //https://youtu.be/dQw4w9WgXcQ?si=g9YQRsH6f1Mlt6cd
    //calling stats from PlayerStats
    [SerializeField] float walkSpeed = PlayerStats.walkSpeed;
    [SerializeField] float jumpForce = PlayerStats.jumpForce;
    [SerializeField] PlayerHealth playerHealth;
    bool onGround;

    Vector2 moveInput;
    Rigidbody rb;

    void Start()
    {
        //setting initial values
        walkSpeed = 10f;
        jumpForce = 4000f;

        //setting the rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
            Run();
        //if(!IsOwner) return; //Makes it so that only the host is controlling a character. Will be deleted later
        if(playerHealth.GetIsDowned()){
            rb.freezeRotation = false;
            walkSpeed = 0;
        }
    }

    //checks which inputs the player has pressed and moves them accordingly
    void Run()
    {
        try{
        Vector3 playerVelocity = new Vector3(moveInput.x * walkSpeed, rb.velocity.y, moveInput.y * walkSpeed);
        rb.velocity = transform.TransformDirection(playerVelocity);
        }catch{

        }

 
    }

    //gets the input values for walking
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    //jumps when jump is called
    private void OnJump()
    {
        if(onGround == true)
        {
            rb.AddForce(0, jumpForce, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Checks collision with floor. Enables you to jump after colliding with floor
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        //Checks collision with floor. This disables your ability to jump if you are not touching the floore
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }

    public void IncrementSpeed(float input)
    {
        walkSpeed += input;
    }
}
