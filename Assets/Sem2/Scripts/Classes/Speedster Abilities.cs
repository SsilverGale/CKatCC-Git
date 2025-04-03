using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SpeedsterAbilities : MonoBehaviour
{
    [SerializeField] GameObject speedsterProjectile;
    Transform cam;
    [SerializeField] Transform weaponTip;
    [SerializeField] Transform weaponTip2;
    [SerializeField] private bool enableShoot;
    bool canDoubleJump = false;
    bool isGrounded = true;
    Rigidbody rb;
    [SerializeField] float jumpForce = 55f;
    UI ui;
    KarbineAnimations KA;
    bool enableRegen = false;
    public bool enableDash = false;
    [SerializeField] bool enableDoubleJump = false;
    [SerializeField] int dashCount = 0;
    public int maxDashCount = 0;
    public bool dashReload = false;
    RaycastHit hit;
    Vector3 rayHit;
    public float dashForce = 15;
    

    void Start()
    {
        KA = GetComponent<KarbineAnimations>();
        ui = GameObject.FindWithTag("UI").GetComponent<UI>();
        jumpForce = 55f;
        rb = GetComponent<Rigidbody>(); 
        enableShoot = true;
        cam = GameObject.FindWithTag("MainCamera").transform;
        cam = GameObject.FindWithTag("MainCamera").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && ui.returnAmmo() == 0)
        {
            KA.StartReload();
            enableShoot = false;
            StartCoroutine(Reload());
        }
        if (Input.GetMouseButton(0) && ui.returnAmmo() != 0 && enableShoot)
        {
                KA.StartShoot();
                Instantiate(speedsterProjectile, weaponTip.position, Quaternion.identity);
                Instantiate(speedsterProjectile, weaponTip2.position, Quaternion.identity);
                SoundManager.PlaySound(SoundType.SPSHOOT);
                ui.decreaseAmmo(2);
                StartCoroutine(Cooldown());

        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            SoundManager.PlaySound(SoundType.JUMP);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == false && canDoubleJump && enableDoubleJump)
        {
            SoundManager.PlaySound(SoundType.JUMP);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canDoubleJump = false;
        }               
        if (enableRegen)
        {
            GetComponent<PlayerHealth>().HealPlayer(0.001f);
        }
        if (dashCount < maxDashCount && dashReload)
        {
            StartCoroutine(DashReload());
        }
  
    }

    void FixedUpdate()
    {
        
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, Camera.main.transform.forward * hit.distance, Color.yellow);
            Debug.Log(hit.collider);
            rayHit = hit.point;
        }

    }

    void OnFire()
    {
    }

    private IEnumerator Cooldown()
    {
        enableShoot = false;
        yield return new WaitForSeconds(0.05f);
        enableShoot = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isGrounded = true;
            canDoubleJump = false;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isGrounded = false;
            canDoubleJump = true;
        }
    }

    IEnumerator DashReload()
    {
        dashReload = false;
        yield return new WaitForSeconds(3);
        ui.UpdateDash(dashCount);
        dashCount++;
        dashReload = true;
    }

    public void LevelSkill(string input)
    {

        if (input == "SPDUP")
        {
            GetComponent<PlayerMove>().IncrementSpeed(1);
        }
        if (input == "HPREGEN")
        {
            enableRegen = true;
        }
        if (input == "MAXHPUP")
        {
            GetComponent<PlayerHealth>().UpdateMaxHP(125);
        }
        if (input == "DASH")
        {
            maxDashCount = 1;
            dashReload = true;
            enableDash = true;
        }
        if (input == "DASH+")
        {
            dashForce += 2;
        }
        if (input == "DBJUMP")
        {
            enableDoubleJump = true;
        }
        if (input == "DASHCH")
        {
            maxDashCount++;
        }
        if (input == "JUMP+")
        {
            jumpForce += 2;
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        enableShoot = true;
    }

    public Vector3 ReturnRayHit()
    {
        return rayHit;
    }
    
    public float ReturnDashForce()
    {
        return dashForce;
    }

    public bool ReturnCanDash()
    {
        return enableDash;
    }

    public int ReturnDashCount()
    {
        return dashCount;
    }

    public int ReturnMaxDash()
    {
        return maxDashCount;
    }
   
}
