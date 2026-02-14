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
            playerMovement.Move(new Vector2(playerInput.horizontalDirection * playerMovement.speed, playerMovement.velocity.y));
        }

        if (playerState.isAllowedDefaultJump)
        {
            playerJump.TryJump(playerCollision.isOnSurface, playerInput.isJumpPressed);
        }

        playerAnimation.ApplyMovementAnimation(playerMovement.velocity.x);
        playerAnimation.ApplyJumpAnimation(playerMovement.velocity.y);
        playerVisuals.ApplyFaceDirection(new Vector2(playerInput.horizontalDirection, 0));
    }
}
