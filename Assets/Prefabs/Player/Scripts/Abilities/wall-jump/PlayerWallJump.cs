using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    PlayerState playerState;
    PlayerMovement playerMovement;
    PlayerInput playerInput;
    PlayerJump playerJump;
    PlayerVisuals playerVisuals;
    PlayerCollision playerCollision;
    [SerializeField] Vector2 wallJumpForce = new Vector2(7, 18);
    [SerializeField] public float pauseDefaultMovementDuration = 0.3f;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
        playerJump = GetComponent<PlayerJump>();
        playerCollision = GetComponent<PlayerCollision>();
        playerState = GetComponent<PlayerState>();
        playerVisuals = GetComponent<PlayerVisuals>();
    }

    void LateUpdate()
    {
        ApplyWallJump();
    }

    public void ApplyWallJump()
    {
        if (playerCollision.isOnWall(playerVisuals.faceDirection.x) && playerInput.isJumpPressed)
        {
            playerState.PauseDefaultMovement(pauseDefaultMovementDuration);
            Vector2 directionMovement = new Vector2(playerVisuals.faceDirection.x * wallJumpForce.x * -1, wallJumpForce.y);
            playerMovement.Move(directionMovement);
            playerVisuals.FlipX();
            playerJump.ResetJumpCount();
            playerJump.IncrementJumpCount();
        }
    }
}
