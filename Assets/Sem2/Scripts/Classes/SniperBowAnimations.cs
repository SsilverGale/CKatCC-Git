using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBowAnimations : MonoBehaviour
{

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Animator>();
    }

    public void DrawString()
    {
        animator.SetBool("IsDraw",true);
    }

    public void Shoot()
    {
        animator.SetBool("IsDraw", false);
        animator.SetBool("IsShoot",true);
        Invoke("StopShoot", 0.8f);
    }

    public void StopShoot()
    {
        animator.SetBool("IsShoot", false);
    }
}
