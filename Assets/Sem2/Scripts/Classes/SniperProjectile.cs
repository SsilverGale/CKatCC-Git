using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperProjectile : MonoBehaviour
{
    Vector3 TargetPos;
    Rigidbody rb;
    float speed = 55f;
    Transform Camera;
    Transform Player;
    CapsuleCollider collider;
    SniperAbilities SA;
    [SerializeField] GameObject moly;

    // Start is called before the first frame update
    void Start()
    {
        SA = GameObject.FindWithTag("Player").GetComponent<SniperAbilities>();
        collider = GetComponent<CapsuleCollider>(); 
        Player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        Camera = GameObject.FindWithTag("MainCamera").transform;
        Ray ray = new Ray(Player.position, Camera.forward);
        rb.AddForce(ray.direction * (speed * SA.getHoldDown()), ForceMode.Impulse);
        transform.rotation = Quaternion.LookRotation(ray.direction) * new Quaternion(90, 0, 0, 90);
    }

    //destroys object on collision other than other players
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "BurgerDetectRange" || other.transform.tag == "HotDogDetectRange" || other.transform.tag == "PopcornDetectRange" || other.transform.tag == "Untagged")
        {
            return;
        }
        else
        {                     
            if (transform.name == "SniperProjectileFire(Clone)")
            {
                Instantiate(moly, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                Instantiate(moly, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                Instantiate(moly, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                Instantiate(moly, transform.position + new Vector3(0,0.2f,0), Quaternion.identity);
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
        
    }
}
