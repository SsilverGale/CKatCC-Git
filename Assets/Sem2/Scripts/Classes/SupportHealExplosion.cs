using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportHealExplosion : MonoBehaviour
{
    Transform transform;
    void Start()
    {
        transform = GetComponent<Transform>();
        Kill();
    }

    void Update()
    {
        if (transform.localScale.x < 10)
        {
            transform.localScale = transform.localScale * 1.11f;
        }

    }

    void Kill()
    {
        Destroy(this.gameObject, 0.15f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        
    }
}
