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
    [SerializeField] public bool isWallJumping { get; private set; } = false;

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
        float duration = 0.3f;

        if (playerCollision.isOnWall(playerVisuals.faceDirection.x) && playerInput.isJumpPressed)
        {
            Vector2 directionMovement = new Vector2(playerVisuals.faceDirection.x * wallJumpForce.x * -1, wallJumpForce.y);

            isWallJumping = true;
            StartCoroutine(playerState.PauseDefaultMovement(duration, () =>
            {
                isWallJumping = false;
            }));

            playerMovement.MoveTowards(directionMovement);
            playerVisuals.FlipX();
            playerJump.ResetJumpCount();
            playerJump.IncrementJumpCount();
        }
    }
}
