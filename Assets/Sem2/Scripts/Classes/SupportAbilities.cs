using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportAbilities : MonoBehaviour
{
    [SerializeField] GameObject SupportProjectile;
    [SerializeField] GameObject RangedHeal;
    Transform Camera;
    bool alternateFist;
    bool enableShoot = true;
    [SerializeField] Transform Fist1;
    [SerializeField] Transform Fist2;
    bool disableAbilities = false;
    PlayerHealth pH;

    // Start is called before the first frame update
    void Start()
    {
        pH = transform.GetComponent<PlayerHealth>();
        Camera = GameObject.FindWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (pH.GetIsDowned() == false)
        {
            if (Input.GetMouseButton(0) && enableShoot)
            {
                Shoot();
                Cooldown();
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Instantiate(RangedHeal, transform.position, Quaternion.identity);
            }
        }
    }

    //shoot function that alternates between fists depending on the value of alternatefist bool
    public void Shoot()
    {
        if (alternateFist) 
        {
            Instantiate(SupportProjectile, Fist1.position, Quaternion.identity);
        }
        if (!alternateFist)
        {
            Instantiate(SupportProjectile, Fist2.position, Quaternion.identity);
        }
        alternateFist = !alternateFist;
    }

    //cooldown to disable then invoke a reenable of shooting
    public void Cooldown()
    {
        enableShoot = false;
        Invoke("EnableShoot", 0.75f);
    }

    //reenable shoot
    public void EnableShoot()
    {
        enableShoot = true;
    }

    public void LevelSkill(string input)
    {

    }
}
