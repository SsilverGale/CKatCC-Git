using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using System;
using UnityEngine.EventSystems;

public class PlayerMove : NetworkBehaviour
{
    //https://youtu.be/dQw4w9WgXcQ?si=g9YQRsH6f1Mlt6cd
    //calling stats from PlayerStats
    [SerializeField] float walkSpeed = PlayerStats.walkSpeed;
    [SerializeField] float jumpForce = PlayerStats.jumpForce;
    [SerializeField] PlayerHealth playerHealth;
    bool onGround;
    public float dashForceX = 15;
    public float dashVelocityX;
    public float dashFallOffDuration = 0.6f;
    private Vector3 _moveDirection;
    public float dashDuration = 0.25f;
    public float dashTime = 0.5f;
    public bool isDashing;
    float residueSpeedX;

    Vector2 moveInput;
    Rigidbody rb;

    void Start()
    {
        //setting initial values
        walkSpeed = 10f;
        jumpForce = 4000f;
        playerHealth = GetComponent<PlayerHealth>();
        //setting the rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        //if(!IsOwner) return; //Makes it so that only the host is controlling a character. Will be deleted later
        if (playerHealth.GetIsDowned()){
            rb.freezeRotation = false;
            walkSpeed = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashTime = 0;
            isDashing = true;
        }
    }

    void FixedUpdate()
    {
        if (GetComponent<SpeedsterAbilities>().ReturnDashCount() > 0 && GetComponent<SpeedsterAbilities>().ReturnCanDash() && transform.parent.name == "SpeedsterPlayer(Clone)")
        {
            Debug.Log("Function runs");
            if (isDashing)
            {
                Debug.Log("Dash");
                dashTime += Time.fixedDeltaTime;
                rb.velocity = new Vector3(dashVelocityX, rb.velocity.y, rb.velocity.z * dashVelocityX);
                if (dashForceX > 10)
                {
                    dashVelocityX = Mathf.Lerp(dashVelocityX, 0, dashTime / dashFallOffDuration);
                }
                if (dashTime >= dashDuration)
                {
                    dashForceX = 15;
                    isDashing = false;
                    residueSpeedX = dashVelocityX;
                }
            }
            else if (residueSpeedX != 0)
            {
                residueSpeedX = Mathf.MoveTowards(residueSpeedX, 0, 1);
                _moveDirection = new Vector3(moveInput.x * walkSpeed, rb.velocity.y, moveInput.y * walkSpeed);
                rb.velocity = transform.TransformDirection(_moveDirection);
            }
            
        }
        else
        {
            _moveDirection = new Vector3(moveInput.x * walkSpeed, rb.velocity.y, moveInput.y * walkSpeed);
            rb.velocity = transform.TransformDirection(_moveDirection);
        }
    }

    /*checks which inputs the player has pressed and moves them accordingly
    void Run()
    {
        try{
        Vector3 playerVelocity = new Vector3(moveInput.x * walkSpeed, rb.velocity.y, moveInput.y * walkSpeed);
        rb.velocity = transform.TransformDirection(playerVelocity);
        }catch{

        }

 
    }
    */

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
