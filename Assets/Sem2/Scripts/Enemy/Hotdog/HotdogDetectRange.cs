using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotdogDetectRange : MonoBehaviour
{
    //variable to get Attack function
    [SerializeField] HotdogAttack ha;
    //variable to stop burger movement
    [SerializeField] HotdogNavi hn;
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
            hn.StopMotion();
            ha.Attack();
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
