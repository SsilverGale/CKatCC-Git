using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarbineAnimations : MonoBehaviour
{
    Animator animator;
    Animator animator2;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Animator>();
        animator2 = transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Animator>();
    }

    public void StartShoot()
    {
        animator.SetBool("IsShoot", true);
        animator2.SetBool("IsShoot", true);
        Invoke("StopShoot", 0.05f);      
        Invoke("StopShoot", 0.05f);
    }

    public void StopShoot()
    {
        animator.SetBool("IsShoot", false);
        animator2.SetBool("IsShoot", false);
    }
}
