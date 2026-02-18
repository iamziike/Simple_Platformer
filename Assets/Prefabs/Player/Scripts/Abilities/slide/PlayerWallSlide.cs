using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlide : MonoBehaviour
{
    [SerializeField] public float wallSlideSpeed = -2f;
    [SerializeField] public bool isSliding { get; private set; } = false;

    private PlayerVisuals playerVisuals;
    private PlayerCollision playerCollision;
    private PlayerWallJump playerWallJump;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerCollision = GetComponent<PlayerCollision>();
        playerVisuals = GetComponent<PlayerVisuals>();
        playerMovement = GetComponent<PlayerMovement>();
        playerWallJump = GetComponent<PlayerWallJump>();
    }

    private void Update()
    {
        if (canSlide && !playerWallJump.isWallJumping)
        {
            isSliding = true;
            SlideWall();
        }
        else
        {
            isSliding = false;
        }
    }

    public bool canSlide => playerCollision.isFalling && playerCollision.isOnWall(playerVisuals.faceDirection.x);

    private void SlideWall()
    {
        playerMovement.MoveTowards(new Vector2(playerMovement.velocity.x, wallSlideSpeed));
    }
}
