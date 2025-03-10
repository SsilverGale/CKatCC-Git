using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankRocket : MonoBehaviour
{
    Vector3 TargetPos;
    Rigidbody rb;
    float speed = 30f;
    Transform Camera;
    Transform Player;
    [SerializeField] GameObject TankRocketExplosion;


    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        Camera = GameObject.FindWithTag("MainCamera").transform;
        Ray ray = new Ray(Player.position,Camera.forward);
        rb.AddForce(ray.direction * speed, ForceMode.Impulse);
        transform.rotation = Quaternion.LookRotation(ray.direction) * new Quaternion(270,0,0,0);
        //Debug.Log(ray);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Launcher" || other.tag == "Launcher" || other.tag == "MainCamera" || other.tag == "BurgerDetectRange" || other.tag == "TankRocketExplosion")
        {
            return;
        }
        else
        {
            Debug.Log(other.tag);
            Destroy(this.gameObject);
            Instantiate(TankRocketExplosion, transform.position, Quaternion.identity);
        }
            
    }
}
