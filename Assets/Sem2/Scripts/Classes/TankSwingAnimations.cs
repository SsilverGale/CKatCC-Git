using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSwingAnimations : MonoBehaviour
{
    Animator animator;
    Animator Rocket;
    UI ui;
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
        animator = transform.GetChild(1).GetChild(0).GetComponent<Animator>();
        Rocket = transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Animator>();
    }

    public void PlayShoot()
    {
        animator.SetBool("IsShooting", true);
        Invoke("StopShoot",0.9f);
    }
    public void StopShoot()
    {
        animator.SetBool("IsShooting", false);
    }

    public void PlayReload()
    {
        Rocket.SetBool("IsReloading", true);
        animator.SetBool("IsReloading", true);
        Invoke("StopReload",(float)((6- ui.returnAmmo()) * 0.75f));
    }

    public void StopReload()
    {
        animator.SetBool("IsReloading", false);
        Rocket.SetBool("IsReloading", false);
    }

    public void PlaySwingAni()
    {
        animator.SetBool("IsSwinging", true);
    }

    public void StopSwingAni() 
    {
        animator.SetBool("IsSwinging", false);
    }
}
