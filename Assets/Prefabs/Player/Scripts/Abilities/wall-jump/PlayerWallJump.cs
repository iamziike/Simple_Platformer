using UnityEngine;

public class PlayerWallJump : MonoBehaviour
{
    Player player;
    [SerializeField] Vector2 wallJumpForce = new Vector2(7, 18);
    [SerializeField] public bool isWallJumping { get; private set; } = false;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    void LateUpdate()
    {
        ApplyWallJump();
    }

    public void ApplyWallJump()
    {
        float duration = 0.3f;

        if (player.collision.isOnWall && player.input.isJumpPressed)
        {
            Vector2 directionMovement = new Vector2(player.collision.faceDirection.x * wallJumpForce.x * -1, wallJumpForce.y);

            isWallJumping = true;
            StartCoroutine(player.PauseDefaultMovement(duration, () =>
            {
                isWallJumping = false;
            }));

            player.movement.MoveTowards(directionMovement);
            player.collision.FlipX();
            player.jump.ResetJumpCount();
            player.jump.IncrementJumpCount();
        }
    }
}
