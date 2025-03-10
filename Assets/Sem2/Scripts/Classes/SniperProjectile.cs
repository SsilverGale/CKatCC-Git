using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SniperProjectile : MonoBehaviour
{
    Vector3 TargetPos;
    Rigidbody rb;
    float speed = 55f;
    Transform Camera;
    Transform Player;
    CapsuleCollider collider;
    SniperAbilities SA;
    [SerializeField] GameObject moly;

    // Start is called before the first frame update
    void Start()
    {
        
        SA = GameObject.FindWithTag("Player").GetComponent<SniperAbilities>();
        int random = Random.Range(0, 100);
        if (random >= 0 && random <= 100 * (SA.ReturnCritModifier() / 100))
        {
            GetComponent<DamageHolder>().SetDamage(GetComponent<DamageHolder>().GetDamage() * SA.GetCritDMG());
        }
        collider = GetComponent<CapsuleCollider>(); 
        Player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        Camera = GameObject.FindWithTag("MainCamera").transform;
        Ray ray = new Ray(Player.position, Camera.forward);
        rb.AddForce(ray.direction * (speed * SA.getHoldDown()), ForceMode.Impulse);
        transform.rotation = Quaternion.LookRotation(Player.transform.forward) * new Quaternion(-90, 0 , 0, 90);
    }

    //destroys object on collision other than other players
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player" || other.transform.tag == "BurgerDetectRange" || other.transform.tag == "HotDogDetectRange" || other.transform.tag == "PopcornDetectRange" || other.transform.tag == "Untagged")
        {
            return;
        }
        else
        {                     
            if (transform.name == "SniperProjectileFire(Clone)")
            {
                GameObject temp = Instantiate(moly, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                GameObject temp2 = Instantiate(moly, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                GameObject temp3 = Instantiate(moly, transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                GameObject temp4 = Instantiate(moly, transform.position + new Vector3(0,0.2f,0), Quaternion.identity);
                temp.GetComponent<DamageHolder>().SetDamage(temp.GetComponent<DamageHolder>().GetDamage() * SA.GetFireMod());
                temp2.GetComponent<DamageHolder>().SetDamage(temp.GetComponent<DamageHolder>().GetDamage() * SA.GetFireMod());
                temp3.GetComponent<DamageHolder>().SetDamage(temp.GetComponent<DamageHolder>().GetDamage() * SA.GetFireMod());
                temp4.GetComponent<DamageHolder>().SetDamage(temp.GetComponent<DamageHolder>().GetDamage() * SA.GetFireMod());
                Destroy(gameObject);
            }
            Destroy(gameObject);
        }
        
    }

    public float GetFireMod()
    {
        return SA.GetFireMod();
    }

    public float GetPoisonMod()
    {
        return SA.GetPoisonMod();
    }
}
