using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ApplyMovementAnimation(float x)
    {
        animator.SetFloat("xMovement", x);
    }

    public void ApplyJumpAnimation(float y)
    {
        animator.SetFloat("yMovement", y);
    }

    public void ApplyKnockBackAnimation()
    {
        animator.SetTrigger("KnockBack");
    }
}
