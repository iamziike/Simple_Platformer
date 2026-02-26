using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideAnimation : MonoBehaviour
{
    Animator animator;
    PlayerWallSlide wallSlide;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        wallSlide = GetComponent<PlayerWallSlide>();
    }

    private void Update()
    {
        animator.SetBool("isWallSliding", wallSlide.isSliding);
    }
}
