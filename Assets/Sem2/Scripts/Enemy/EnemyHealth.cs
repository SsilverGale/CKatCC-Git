using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hp;
    float reduceHealth;
    bool enableLauncherDamage = true;
    bool enablePoison = false;
    float captureTime = 0;
    bool enableFire = false;
    XP xp;

    float fireDMG;


    void Start()
    {
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
        if (hp < 0) 
        {
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
            
            
            Destroy(gameObject);
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
        if (collision.gameObject.tag == "SniperProjectile" || collision.gameObject.tag == "SupportPunchProjectile" || collision.gameObject.tag == "TankRocketExplosion" || collision.gameObject.tag == "SpeedsterProjectile" || (collision.gameObject.tag == "Launcher" && enableLauncherDamage))
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
        hp *= 4; 
    }

}
