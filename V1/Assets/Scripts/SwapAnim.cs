using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gebruik ik niet meer
public class AnimationLooper : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            bool currentState = animator.GetBool("SwitchAnimation");
            animator.SetBool("SwitchAnimation", !currentState);
        }
    }
}