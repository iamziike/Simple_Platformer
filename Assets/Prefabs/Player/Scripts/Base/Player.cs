using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Life")]
    [SerializeField] readonly int _maxHealth = 3;
    [SerializeField] int _currentHealth = 3;

    [SerializeField] public bool isAllowedDefaultMovement { get; private set; } = true;
    [SerializeField] public bool isAllowedDefaultJump { get; private set; } = true;
    [SerializeField] public bool canUseAbility { get; private set; } = false;

    public PlayerInput input;
    public PlayerJump jump;
    public PlayerMovement movement;
    public PlayerCollision collision;
    public PlayerAnimation animation;

    public Vector2 LastPosition
    {
        get
        {
            return new Vector2(PlayerPrefs.GetFloat("LastPositionX", transform.position.x), PlayerPrefs.GetFloat("LastPositionY", transform.position.y));
        }
        set
        {
            PlayerPrefs.SetFloat("LastPositionX", value.x);
            PlayerPrefs.SetFloat("LastPositionY", value.y);
        }
    }

    public void ClearLastPosition()
    {
        PlayerPrefs.DeleteKey("LastPositionX");
        PlayerPrefs.DeleteKey("LastPositionY");
    }

    void Awake()
    {
        HandleLoadIn();

        input = GetComponent<PlayerInput>();
        jump = GetComponent<PlayerJump>();
        movement = GetComponent<PlayerMovement>();
        collision = GetComponent<PlayerCollision>();
        animation = GetComponent<PlayerAnimation>();
    }

    void LateUpdate()
    {
        if (isAllowedDefaultMovement && !collision.isDetectedWall(input.horizontalDirection))
        {
            movement.Run(new Vector2(input.horizontalDirection, movement.velocity.y));
        }

        if (isAllowedDefaultJump)
        {
            jump.TryJump(collision.isOnSurface, input.isJumpPressed);
        }

        if (input.horizontalDirection != 0 && input.horizontalDirection != collision.faceDirection.x)
        {
            collision.FlipX();
        }

        animation.ApplyMovementAnimation(movement.velocity.x);
        // When applying velocity to x axis when running for some reason the player y velocity is not 0 hence the below
        animation.ApplyJumpAnimation(collision.isOnSurface ? 0 : movement.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Tag.Checkpoint))
        {
            LastPosition = other.transform.position;
            Checkpoint checkpoint = other.GetComponent<Checkpoint>();

            if (checkpoint.CheckpointType == CheckpointType.End)
            {
                HandleDeath();
                ClearLastPosition();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 collisionDirection = other.GetContact(0).normal;
        bool isOnTopOfObject = collisionDirection.y == 1;

        if (other.gameObject.CompareTag(Constants.Tag.Enemy))
        {
            if (isOnTopOfObject)
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy?.HandleDamageTaken();
                movement.MoveTowards(new Vector2(movement.velocity.x, jump.jumpForce / 1.5f));
            }
            else
            {
                HandleDamageTaken(collisionDirection);
            }
        }
    }

    #region Pause Actions

    public IEnumerator PauseDefaultMovement(float duration, System.Action onResume = null)
    {
        isAllowedDefaultMovement = false;
        yield return new WaitForSeconds(duration);
        isAllowedDefaultMovement = true;
        onResume?.Invoke();
    }

    #endregion

    #region Damage

    public int currentHealth
    {
        get => _currentHealth;
        set => _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
    }

    void HandleGiveDamage()
    {
        // 
    }

    void HandleDamageTaken(Vector2 colliderLocation)
    {
        // currentHealth--;

        // if (currentHealth <= 0)
        // {
        //     HandleDeath();
        // }

        float duration = 0.3f;
        StartCoroutine(PauseDefaultMovement(duration));
        movement.KnockBack(colliderLocation);
        animation.ApplyKnockBackAnimation();
    }

    void HandleLoadIn()
    {
        transform.position = LastPosition;
    }

    void HandleDeath()
    {
        // 
    }

    #endregion
}
