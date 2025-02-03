using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportRangedHeal : MonoBehaviour
{
    Vector3 TargetPos;
    Rigidbody rb;
    float speed = 15f;
    Transform Camera;
    Transform Player;
    [SerializeField] GameObject RangedHealExplosion;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        Camera = GameObject.FindWithTag("MainCamera").transform;
        Ray ray = new Ray(Player.position, Camera.forward);
        rb.AddForce((ray.direction * speed) + new Vector3(0, 1, 0), ForceMode.Impulse);
        transform.rotation = Quaternion.LookRotation(ray.direction) * new Quaternion(90, 0, 0, 90);
    }

    //destroys object if colliding
    public void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
        Instantiate(RangedHealExplosion,transform.position,Quaternion.identity);
    }
}
