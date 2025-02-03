using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportPunchProjectile : MonoBehaviour
{
    Vector3 TargetPos;
    Rigidbody rb;
    float speed = 15f;
    Transform Camera;
    Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        Camera = GameObject.FindWithTag("MainCamera").transform;
        Ray ray = new Ray(Player.position, Camera.forward);
        rb.AddForce(ray.direction * speed, ForceMode.Impulse);
        transform.rotation = Quaternion.LookRotation(ray.direction) * new Quaternion(90, 0, 0, 90);
        Destroy(this.gameObject,0.5f);
    }

    void OnTriggerEnter(Collider other)
    {

    }
}
