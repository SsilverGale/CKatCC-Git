using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedsterProjectile : MonoBehaviour
{
    Vector3 TargetPos;
    Rigidbody rb;
    float speed = 55f;
    Transform Camera;
    Transform Player;
    CapsuleCollider collider;

    float dmgAmp = 1;
    float blltVelo = 1;

    SpeedsterAbilities SA;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        Player = GameObject.FindWithTag("Player").transform;
        SA = GameObject.FindWithTag("Player").GetComponent<SpeedsterAbilities>();
        rb = GetComponent<Rigidbody>();
        Camera = GameObject.FindWithTag("MainCamera").transform;
        rb.AddForce((SA.ReturnRayHit() - Player.transform.position).normalized * speed * blltVelo, ForceMode.Impulse);
        transform.rotation = Quaternion.LookRotation(SA.ReturnRayHit());
    }

    //destroys object on collision other than other players
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "SpeedsterProjectile" || other.transform.tag == "Guntip")
        {
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Destroy(this.gameObject,4f);
    }
}
