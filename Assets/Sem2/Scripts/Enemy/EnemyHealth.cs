using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public float hp;
    float reduceHealth;
    bool enableLauncherDamage = true;
    bool enablePoison = false;
    float captureTime = 0;
    bool enableFire = false;
    XP xp;
    UI ui;
    Rigidbody rb;
    Player player;
    bool first = false;
    bool fly = false;
    bool up = false;

    float fireDMG;

    Color temp;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
        xp = GameObject.FindGameObjectWithTag("XPHolder").GetComponent<XP>();
    }

    void Update()
    {
        if (enablePoison && (Time.time - captureTime) <= 6)
        {
            hp -= 5;
            enablePoison = false;
            StartCoroutine(PoisonTick());
        }
        if (enableFire)
        {
            hp -= fireDMG;
            enableFire = false;
            StartCoroutine(FireTick());
        }
        if (hp < 0 && !first) 
        {
            Vector3 backDirection = -transform.forward;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            
            rb.mass = 0.2f;
            rb.AddForce(backDirection * 500);
            GetComponent<NavMeshAgent>().enabled = false;
            GameObject.FindGameObjectWithTag("WaveSpawn").GetComponent<WaveSpawn>().DecreaseEnemyCount();
            xp.AddXP(15);
            GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i].GetComponent<PlayerXP>().FetchXP();
            }
            temp = GameObject.FindGameObjectsWithTag("UI");
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i].GetComponent<UI>().UpdateXP();
            }
            int random = Random.Range(0,3);
            
            if (random == 0)
            {
                Invoke("Descend", 2);
            }
            if (random == 1)
            {
                fly = true;
                Invoke("Descend", 2);
            }
            if (random == 2)
            {
                up = true;
            }
            first = true;
            Invoke("Descend", 2);
        }
        if (fly)
        {
            Vector3 backDirection = -transform.forward + new Vector3(1,1,0);
            rb.AddForce(backDirection * 5);
        }
        if (up)
        {
            rb.AddForce(Vector3.up * 1);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "")
        {
            reduceHealth = collision.gameObject.GetComponent<DamageHolder>().GetDamage();
            HurtEnemy();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Respawner")
        {
            transform.position = new Vector3(-10,4,0);
        }
        if (collision.gameObject.tag == "SniperProjectile" || collision.gameObject.tag == "SupportPunchProjectile" || collision.gameObject.tag == "TankRocketExplosion" || collision.gameObject.tag == "SpeedsterProjectile" || (collision.gameObject.tag == "Launcher" && enableLauncherDamage) || collision.gameObject.tag == "BearTrap")
        {
            reduceHealth = collision.gameObject.GetComponent<DamageHolder>().GetDamage();
            HurtEnemy();
            if (collision.gameObject.tag == "Launcher")
            {           
                StartCoroutine(LauncherDamageCooldown());
            }
            if (collision.name == "SniperProjectilePoison")
            {
                collision.GetComponent<SniperProjectile>().GetPoisonMod();
                enablePoison = true;
                captureTime = Time.time;
            }
            
        }
        if (collision.transform.tag == "Molotov")
        {
            enableFire = true;
            fireDMG = collision.GetComponent<DamageHolder>().GetDamage();
        }

    }

    public void HurtEnemy()
    {
        hp -= reduceHealth;
        ui.HitMarker();
        if (transform.name =="Borgir(Clone)" || transform.name == "BorgirBoss(Clone)" && hp > 0)
        {
            transform.GetChild(0).GetChild(3).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", new Color(255, 0, 0));
            Invoke("RevertColor", 0.1f);
        }
        if (transform.name == "HotDog(Clone)" || transform.name == "HotDogBoss(Clone)" && hp > 0)
        {
            transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", new Color(255, 0, 0));
            Invoke("RevertColor", 0.1f);
        }
        if (transform.name == "Popcorn Variant(Clone)" || transform.name == "PopcornBoss(Clone)" && hp > 0)
        {
            transform.GetChild(2).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", new Color(255, 0, 0));
            Invoke("RevertColor", 0.1f);
        }

    }

    public void RevertColor()
    {
        if (transform.name == "Borgir(Clone)" || transform.name == "BorgirBoss(Clone)")
        {
            transform.GetChild(0).GetChild(3).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", new Color(1,1,1,1));
        }
        if (transform.name == "HotDog(Clone)" || transform.name == "HotDogBoss(Clone)")
        {
            transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", new Color(1,1,1,1));
        }
        if (transform.name == "Popcorn Variant(Clone)" || transform.name == "PopcornBoss(Clone)")
        {
            transform.GetChild(2).GetChild(1).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", new Color(1,1,1,1));
        }
    }

    IEnumerator LauncherDamageCooldown()
    {
        enableLauncherDamage = false;
        yield return new WaitForSeconds(0.8f);
        enableLauncherDamage = true;
    }

    IEnumerator PoisonTick()
    {       
        yield return new WaitForSeconds(1.5f);
        enablePoison = true;
    }

    IEnumerator FireTick()
    {
        yield return new WaitForSeconds(1.5f);
        enableFire = true;
    }

    public void IncreaseHealth()
    {
        hp *= 3; 
    }

    public void Descend()
    {
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 2f);
        GetComponent<EnemyHealth>().enabled = false;
    }
    public float getHealth()
    {
        return hp;
    }
}
