using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornExplosion : MonoBehaviour
{
    Transform transform;
    void Start()
    {
        transform = GetComponent<Transform>();
        Kill();
    }

    void Update()
    {
        if (transform.localScale.x < 7)
        {
            transform.localScale = transform.localScale * 1.09f;
        }
        
    }

    void Kill()
    {
        Destroy(this.gameObject,0.15f);
    }
}
