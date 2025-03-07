using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpeedsterAbilities : MonoBehaviour
{
    [SerializeField] GameObject speedsterProjectile;
    Transform cam;
    [SerializeField] Transform weaponTip;
    [SerializeField] Transform weaponTip2;
    private bool enableShoot;
    bool canDoubleJump = false;
    bool isGrounded = true;
    Rigidbody rb;
    [SerializeField] float jumpForce = 55f;
    UI ui;
    KarbineAnimations KA;
    bool enableRegen = false;
    bool enableDash = false;
    bool enableDoubleJump = false;
    float dashForce = 5;
    int dashCount = 0;
    int maxDashCount = 0;

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
        if (enableShoot == true && Input.GetMouseButton(0) && ui.returnAmmo() != 0)
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            enableShoot = false;
            StartCoroutine(Reload());
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCount > 0 && enableDash)
        {
            rb.AddForce(Vector3.forward * dashForce, ForceMode.Impulse);
        }
        if (enableRegen)
        {
            GetComponent<PlayerHealth>().HealPlayer(0.001f);
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
}
