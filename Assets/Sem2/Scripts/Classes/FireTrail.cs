using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrail : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] GameObject MolyField;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
        rb = GetComponent<Rigidbody>();
        float random = Random.Range(0,3);
        float random2 = Random.Range(0, 2);
        float random3 = Random.Range(0, 3);
        rb.AddForce(new Vector3(random,random2,random3) + Vector3.up * 2,ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            Instantiate(MolyField, transform.position, Quaternion.identity);
        }
        Destroy(gameObject,2f);
    }

}
