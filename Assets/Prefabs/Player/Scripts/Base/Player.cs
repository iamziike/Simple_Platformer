using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerState playerState;
    public PlayerInput playerInput;
    public PlayerJump playerJump;
    public PlayerMovement playerMovement;
    public PlayerCollision playerCollision;
    public PlayerVisuals playerVisuals;
    public PlayerAnimation playerAnimation;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerJump = GetComponent<PlayerJump>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCollision = GetComponent<PlayerCollision>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerState = GetComponent<PlayerState>();
        playerVisuals = GetComponent<PlayerVisuals>();
    }

    void LateUpdate()
    {
        if (playerState.isAllowedDefaultMovement && !playerCollision.isDetectedWall(playerInput.horizontalDirection))
        {
            playerMovement.Run(new Vector2(playerInput.horizontalDirection, playerMovement.velocity.y));
        }

        if (playerState.isAllowedDefaultJump)
        {
            playerJump.TryJump(playerCollision.isOnSurface, playerInput.isJumpPressed);
        }

        playerVisuals.ApplyFaceDirection(new Vector2(playerInput.horizontalDirection, 0));
        playerAnimation.ApplyMovementAnimation(playerMovement.velocity.x);
        // When applying velocity to x axis when running for some reason the player y velocity is not 0 hence the below
        playerAnimation.ApplyJumpAnimation(playerCollision.isOnSurface ? 0 : playerMovement.velocity.y);
    }
}
