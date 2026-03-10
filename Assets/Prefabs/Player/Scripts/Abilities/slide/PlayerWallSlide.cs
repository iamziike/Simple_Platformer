using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlide : MonoBehaviour
{
    [SerializeField] public float wallSlideSpeed = -2f;
    [SerializeField] public bool isSliding { get; private set; } = false;

    private PlayerCollision collision;
    private PlayerWallJump wallJump;
    private PlayerMovement movement;

    private void Awake()
    {
        collision = GetComponent<PlayerCollision>();
        movement = GetComponent<PlayerMovement>();
        wallJump = GetComponent<PlayerWallJump>();
    }

    private void Update()
    {
        if (collision.isOnSurface || !canSlide)
        {
            isSliding = false;
        }

        if (canSlide && !wallJump.isWallJumping)
        {
            isSliding = true;
            SlideWall();
        }
    }

    public bool canSlide => collision.isFalling && collision.isOnWall;

    private void SlideWall()
    {
        movement.MoveTowards(new Vector2(movement.velocity.x, wallSlideSpeed));
    }
}
