using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideAnimation : MonoBehaviour
{
    Animator animator;
    PlayerWallSlide playerWallSlide;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerWallSlide = GetComponent<PlayerWallSlide>();
    }

    private void Update()
    {
        animator.SetBool("isWallSliding", playerWallSlide.isSliding);
    }
}
