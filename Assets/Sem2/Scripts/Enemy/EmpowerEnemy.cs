using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpowerEnemy : MonoBehaviour
{
    bool isSizingUp = false;
    CapsuleCollider Collider;
    // Start is called before the first frame update
    void Start()
    {
        Collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSizingUp && transform.localScale.x <= 4)
        {
            transform.localScale *= 1.001f;
            Collider.transform.localScale *= 1.001f;

        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "SniperProjectile" || collision.gameObject.tag == "SupportPunchProjectile" || collision.gameObject.tag == "TankRocketExplosion" || collision.gameObject.tag == "SpeedsterProjectile" || (collision.gameObject.tag == "Launcher"))
        {
            transform.parent.GetChild(1).gameObject.SetActive(true);
            if (transform.name == "")
            GetComponent<EnemyHealth>().IncreaseHealth();
            IncreaseSize();
        }
    }

    void IncreaseSize()
    {
        isSizingUp = true;
    }
}
