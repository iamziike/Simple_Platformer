using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerMovement playerMovement;
    PlayerJump playerJump;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();

        SetupEventListeners();
    }

    void SetupEventListeners()
    {

    }

    void LateUpdate()
    {
        playerMovement.ApplyHorizontalMovement(playerInput.direction.x);
        playerJump.ApplyJump(playerInput.isJumpPressed, playerMovement.isOnSurface, playerMovement.ApplyVerticalMovement);
    }
}
