using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceObjective : MonoBehaviour
{
    bool isInvincible = false;
    float oHealth = 500;
    float reduceHealth;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Oucher" || collision.gameObject.tag == "BurgerEnemy" || collision.gameObject.tag == "PopcornExplosion" || collision.gameObject.tag == "HotdogBullet") && isInvincible == false)
        {
            reduceHealth = collision.gameObject.GetComponent<DamageHolder>().GetDamage();
            StartCoroutine(HurtPlayer());
        }

    }

    IEnumerator HurtPlayer()
    {
        isInvincible = true;
        oHealth -= reduceHealth;
        Debug.Log(oHealth);
        yield return new WaitForSeconds(0.2f);
        isInvincible = false;
    }
}
