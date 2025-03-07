using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //calling stats from PlayerStats
    [SerializeField] float pHealth = PlayerStats.pHealth;
    float reduceHealth;

    bool isInvincible;

    bool isDowned = false;

    bool enableRegen = false;

    bool hit = false;
    void Start()
    {
        pHealth = 100f;
        isInvincible = false;
    }

    void Update()
    {
        if (hit)
        {
            StartCoroutine(RegenReset());
        }
        if (enableRegen && pHealth < PlayerStats.pHealth)
        {
            pHealth += 0.001f;
        }
        if (pHealth <= 0)
        {
            isDowned = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Oucher" || collision.gameObject.tag == "BurgerEnemy" || collision.gameObject.tag == "PopcornExplosion" || collision.gameObject.tag == "HotdogBullet" || collision.gameObject.tag == "FIHS") && isInvincible == false)
        {
            reduceHealth = collision.gameObject.GetComponent<DamageHolder>().GetDamage();
            hit = true;
            StartCoroutine(HurtPlayer());
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.tag == "Oucher" || collision.gameObject.tag == "BurgerEnemy" || collision.gameObject.tag == "PopcornExplosion" || collision.gameObject.tag == "HotdogBullet" || collision.gameObject.tag == "FIHS") && isInvincible == false)
        {
            reduceHealth = collision.gameObject.GetComponent<DamageHolder>().GetDamage();
            hit = true;
            StartCoroutine(HurtPlayer());
        }
        if (collision.gameObject.tag == "SupportHealExplosion")
        {
            HealPlayer(25);
        }

    }

    IEnumerator HurtPlayer()
    {
        isInvincible = true;
        pHealth -= reduceHealth;
        Debug.Log(pHealth);
        yield return new WaitForSeconds(0.2f);
        isInvincible = false;
    }

    IEnumerator RegenReset()
    {
        enableRegen = false;
        yield return new WaitForSeconds(5f);
        enableRegen = true;
    }

    public void HealPlayer(float healAmount)
    {
        pHealth += healAmount;
        Debug.Log(pHealth);
    }

    public bool GetIsDowned()
    {
        return isDowned;
    }

    public float getHp()
    {
        return pHealth;
    }

    public void IncrementHealth(float input)
    {
        pHealth += input;
    }

    public void UpdateMaxHP(float input)
    {
        pHealth = input;
    }
}
