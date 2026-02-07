using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float jumpForce = 15;
    [SerializeField] int jumpCount = 0;
    [SerializeField] int jumpLimit = 1;

    [Header("Buffer Time")]
    [SerializeField] float bufferTime = 0;
    [SerializeField] float bufferTimeLimit = 0.25f;


    public void ApplyJump(bool isJumpPressed, bool isOnSurface, System.Action<float> applyVelocity)
    {
        if (isOnSurface)
        {
            jumpCount = 0;
        }

        if (isJumpPressed)
        {
            bufferTime = Time.time;
        }

        if (isJumpPressed || (isOnSurface && !Utils.HasTimeElapsed(bufferTime + bufferTimeLimit, Time.time)))
        {
            if (jumpCount < jumpLimit)
            {
                jumpCount++;
                applyVelocity?.Invoke(jumpForce);
            }
        }
    }
}
