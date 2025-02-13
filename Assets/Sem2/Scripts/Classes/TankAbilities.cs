using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TankAbilities : MonoBehaviour
{
    Transform PlayerTransform;
    Transform ColliderTransform;
    Rigidbody rb;
    bool isBig = false;
    bool growing = false;
    bool shrinking = false;
    [SerializeField] GameObject TankBullet;
    Transform LauncherTip;
    Transform Camera;
    TankSwingAnimations TSA;
    bool disableAbilities = false;
    bool isGrounded;
    PlayerHealth pH;
    bool enableSwingSound = true;
    bool enableShoot = true;
    UI ui;
    

    void Start()
    {
        ui = GameObject.FindWithTag("UI").GetComponent<UI>();
        pH = transform.GetComponent<PlayerHealth>();
        TSA = GetComponent<TankSwingAnimations>();
        Camera = transform.parent.GetChild(0).transform;
        LauncherTip = transform.GetChild(1).GetChild(0).GetChild(0).gameObject.transform;
        rb = GetComponent<Rigidbody>();
        ColliderTransform = GetComponent<CapsuleCollider>().transform;
        PlayerTransform = GetComponent<Transform>();
    }

   
    void Update()
    {
        if (!isBig)
        {
            transform.GetChild(1).transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
        }
        if (isBig)
        {
            transform.GetChild(1).transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
        }
        if (pH.GetIsDowned() == false)
        {
            //get shift key press and change if tank is growing or shrinking
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (isBig)
                {
                    gameObject.transform.GetChild(1).GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
                    shrinking = true;
                    Camera.transform.position -= new Vector3(0.1f, 1.8f, 2);
                }
                if (!isBig)
                {
                    gameObject.transform.GetChild(1).GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
                    growing = true;
                    Camera.transform.position += new Vector3(0.1f, 1.8f, 2);
                }
                

            }
        if (Input.GetMouseButtonDown(0) && !isBig && enableShoot && ui.returnAmmo() != 0)
        {
            SoundManager.PlaySound(SoundType.TSHOOT);
            TSA.PlayShoot();
            ui.decreaseAmmo(1);
            Shoot();
            StartCoroutine(RocketCooldown());
        }

        if (Input.GetMouseButton(0) && isBig)
        {
                if (enableSwingSound)
                {
                    SoundManager.PlaySound(SoundType.TSWING);
                    StartCoroutine(SwingCooldown());
                }
                Swing();
        }
        if (Input.GetMouseButtonUp(0) && isBig)
        {
            StopSwing();
        }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                SoundManager.PlaySound(SoundType.JUMP);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            enableShoot = false;
            TSA.PlayReload();
            StartCoroutine(Reload());
        }

        //grows player
        if (growing && PlayerTransform.transform.localScale.x <= 3) 
        {
            Debug.Log("growing");   
            PlayerTransform.transform.localScale *= 1.001f;
            ColliderTransform.localScale *= 1.001f;
            rb.isKinematic = false;
            
            if (PlayerTransform.transform.localScale.x >= 3)
            {
                growing = false;
                isBig = true;                
            }
        }
        
        //shrinks player
        if (shrinking && PlayerTransform.transform.localScale.x >= 0.5f)
        {
            Debug.Log("shrinking");
            PlayerTransform.transform.localScale *= 0.999f;
            ColliderTransform.localScale *= 0.999f;
            rb.isKinematic = false;
            if (PlayerTransform.transform.localScale.x <= 0.5f)
            {
                shrinking = false;
                isBig = false;    
            }
        }
    }

    public void Shoot()
    {
        Instantiate(TankBullet, LauncherTip.position, Quaternion.identity);
    }

    public void Swing()
    {
        TSA.PlaySwingAni();
    }
    public void StopSwing()
    {
        TSA.StopSwingAni();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    IEnumerator SwingCooldown()
    {
        enableSwingSound = false;
        yield return new WaitForSeconds(0.8f);
        enableSwingSound = true;
    }

    IEnumerator RocketCooldown()
    {
        enableShoot = false;
        yield return new WaitForSeconds(1.5f);
        enableShoot = true;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(4);
        enableShoot = true;
    }

    public void LevelSkill(string input)
    {

    }
}
