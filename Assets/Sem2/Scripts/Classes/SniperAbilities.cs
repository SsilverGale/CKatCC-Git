using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SniperAbilities : MonoBehaviour
{
    [SerializeField] GameObject SniperProjectile;
    Transform Camera;
    [SerializeField] Transform weaponTip;
    bool enableShoot = true;
    bool disableAbilities = false;
    PlayerHealth pH;
    bool isGrounded = true;
    [SerializeField] float chargeTime = 0;
    float capturedTime;
    SniperBowAnimations SBA;
    [SerializeField] int projectileCycle = 0;
    GameObject currentProjectile;
    [SerializeField] GameObject[] arrowSet = new GameObject[2];

    [SerializeField] float dmgAmp = 1;
    float reloadCDN = 1;
    float blltVelo = 1;



    // Start is called before the first frame update
    void Start()
    {
        currentProjectile = arrowSet[0];
        SBA = GetComponent<SniperBowAnimations>();
        pH = transform.GetComponent<PlayerHealth>();
        Camera = GameObject.FindWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (pH.GetIsDowned() == false)
        {
            if (Input.GetMouseButtonDown(0) && enableShoot)
            {
                capturedTime = Time.time;
                SBA.DrawString();
            }
            if (Input.GetMouseButtonUp(0) && enableShoot)
            {
                SBA.Shoot();
                chargeTime = Time.time - capturedTime;
                SoundManager.PlaySound(SoundType.SNSHOOT);
                Instantiate(currentProjectile, weaponTip.position, Quaternion.identity);
                Cooldown();
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                SwapArrow();
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                SoundManager.PlaySound(SoundType.JUMP);
            }
        }


    }

    //cooldown funciton to disable shooting and to enable shooting 1sec later
    public void Cooldown()
    {
        enableShoot = false;
        Invoke("EnableShoot", 1.5f);
    }

    //function to enable shooting from cooldown function
    public void EnableShoot()
    {
        enableShoot = true;
    }

    //swap arrow function
    public void SwapArrow()
    {
        projectileCycle++;
        if (projectileCycle >= 3)
        {
            projectileCycle = 0;
        }
        currentProjectile = arrowSet[projectileCycle];
        if (projectileCycle == 0)
        {
            transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).GetChild(1).gameObject.SetActive(false);
        }
        if (projectileCycle == 1)
        {
            transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).GetChild(1).gameObject.SetActive(false);
        }
        if (projectileCycle == 2)
        {
            transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).GetChild(1).gameObject.SetActive(true);
            transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).GetChild(0).gameObject.SetActive(false);
        }

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

    public float getHoldDown()
    {
        if (chargeTime >= 1.5f)
        {
            chargeTime = 1.5f;
        }
        return chargeTime;
    }

    public void LevelSkill(string input)
    {
        if (input == "DMGUP")
        {
            dmgAmp += 0.05f;
        }
        if (input == "BLLTVELOUP")
        {
            blltVelo += 0.05f;
        }
        if (input == "RLDDWN")
        {
            reloadCDN -= 0.05f;
        }
        if (input == "AMMOUP")
        {

        }
        if (input == "HPUP")
        {
            GetComponent<PlayerHealth>().IncrementHealth(5);
        }
        if (input == "SPDUP")
        {
            GetComponent<PlayerMove>().IncrementSpeed(1);
        }
    }

    public float ReturnAmp()
    {
        return dmgAmp;
    }
    public float ReturnVelocity()
    {
        return blltVelo;
    }
    public float ReturnReload()
    {
        return reloadCDN;
    }
}
