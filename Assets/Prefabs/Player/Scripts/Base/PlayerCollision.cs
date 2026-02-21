using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    [SerializeField] public bool isFalling { get; private set; }
    [SerializeField] public bool isOnSurface { get; private set; }
    [SerializeField] public float groundCheckDistance = 1.05f;
    [SerializeField] public float wallCheckDistance = 0.6f;
    [SerializeField] public float wallDirection { get; private set; }

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleDetectGround();
        HandleDetectFalling();
    }
    public bool isDetectedWall(float direction)
    {
        Vector2 origin = transform.position;
        Vector2 directionVector = new Vector2(direction, 0);
        RaycastHit2D hit = Physics2D.Raycast(origin, directionVector, wallCheckDistance, LayerMask.GetMask(Constants.LayerMask.Ground));

        if (hit.collider != null)
        {
            wallDirection = direction;
            return true;
        }
        else
        {
            wallDirection = 0;
            return false;
        }

        // show raycast for debugging
    }

    public bool isOnWall(float direction)
    {
        return isDetectedWall(direction) && !isOnSurface;
    }

    void HandleDetectGround()
    {
        bool isNoVelocity = rigidbody2D.velocity.y < 0.01 && rigidbody2D.velocity.y > -0.01;
        Vector2 origin = transform.position;
        Vector2 directionVector = Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(origin, directionVector, groundCheckDistance, LayerMask.GetMask(Constants.LayerMask.Ground));
        isOnSurface = hit.collider != null && isNoVelocity;
    }

    void HandleDetectFalling()
    {
        float checkPoint = 0.01f;
        isFalling = rigidbody2D.velocity.y < -checkPoint;
    }


}
