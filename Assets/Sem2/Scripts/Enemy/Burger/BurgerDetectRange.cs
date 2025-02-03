using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UIElements;

public class BurgerDetectRange : MonoBehaviour
{
    //variable to get Attack function
    [SerializeField] BurgerAttack ba;
    //variable to stop burger movement
    [SerializeField]  BurgerNavi bn;
    //checks collision with player
    Rigidbody rb;

    //get rigidbody
    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    //checks if player is in burger's range
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GetComponent<SphereCollider>().enabled = false;
            //Debug.Log("Player in range");
            bn.StopMotion();
            ba.Attack();
            other = null;
        }
    }

    //reenable collider
    public void EnableCollider()
    {
        GetComponent<SphereCollider>().enabled = true;
        rb.velocity = Vector3.zero;
    }


}
