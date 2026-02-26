using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyAnimation : MonoBehaviour
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

    public void ApplyDamageTakenAnimation()
    {
        animator.SetTrigger("DamageTaken");
    }
}
