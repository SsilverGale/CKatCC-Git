using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class HotDogBullet : MonoBehaviour
{
    Ray ray;
    Transform playerTransform;
    Vector3 TargetPos;
    Rigidbody rb;
    float speed = 30f;
    Transform enemyTransform;

    void Start()
    {
        enemyTransform = GetComponentInParent<Transform>();
        rb = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        TargetPos = playerTransform.position;
        Vector3 newDir = TargetPos - enemyTransform.position;
        ray = new Ray(enemyTransform.position, newDir);
        rb.AddForce(ray.direction * speed, ForceMode.Impulse);
        //Debug.Log(ray);
    }
    
}
