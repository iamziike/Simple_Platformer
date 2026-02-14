using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    [SerializeField] float jumpForce = 15;
    [SerializeField] int jumpCount = 0;
    [SerializeField] int jumpLimit = 2;

    [Header("Jump Buffer")]
    [SerializeField] float bufferTime = 0;
    [SerializeField] float bufferTimeLimit = 0.25f;

    public bool isJumpCached => bufferTime != 0 && !Utils.HasTimeElapsed(bufferTime + bufferTimeLimit, Time.time);

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void ResetJumpCount()
    {
        jumpCount = 0;
    }

    public void IncrementJumpCount()
    {
        jumpCount++;
    }

    public void ResetJumpBuffer()
    {
        bufferTime = 0;
    }

    public void CacheJump()
    {
        bufferTime = Time.time;
    }

    public void Jump()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
        IncrementJumpCount();
    }

    public void TryJump(bool isOnSurface = false, bool isJumpPressed = false)
    {
        if (isOnSurface)
        {
            ResetJumpCount();
        }

        if ((isJumpCached && isOnSurface) || isJumpPressed)
        {
            if (jumpCount < jumpLimit)
            {
                Jump();
                ResetJumpBuffer();
            }
            else
            {
                CacheJump();
            }
        }
    }
}
