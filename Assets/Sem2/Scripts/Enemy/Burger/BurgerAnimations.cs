using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerAnimations : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void PlayWalk()
    {
        animator.SetBool("IsWalking",true);
        animator.SetBool("IsIdle", false);
    }

    public void StopWalk()
    {
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsIdle", true);
    }
}
