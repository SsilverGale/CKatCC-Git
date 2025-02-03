using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopcornAnimations : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(2).GetComponent<Animator>();
    }

    public void PlayWalk()
    {
        animator.SetBool("IsWalk",true);
        animator.SetBool("IsIdle", false);
    }

    public void StopWalk()
    {
        animator.SetBool("IsIdle", true);
        animator.SetBool("IsWalk", false);
    }
}
