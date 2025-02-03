using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedsterProjectile : MonoBehaviour
{
    Vector3 TargetPos;
    Rigidbody rb;
    float speed = 35f;
    Transform Camera;
    Transform Player;
    CapsuleCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        Player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        Camera = GameObject.FindWithTag("MainCamera").transform;
        Ray ray = new Ray(Player.position, Camera.forward);
        rb.AddForce(ray.direction * speed, ForceMode.Impulse);
        transform.rotation = Quaternion.LookRotation(ray.direction) * new Quaternion(90, 0, 0, 90);
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
