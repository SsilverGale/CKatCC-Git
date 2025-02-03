using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void OnCollisionEnter(Collision collision)
    {
        //physics based on if the player is hit and what velocity the burger has
        if (collision.transform.CompareTag("BurgerEnemy"))
        {
            
            transform.GetComponentInParent<Rigidbody>().velocity = collision.transform.GetComponent<Rigidbody>().velocity;
        }
    }
}
