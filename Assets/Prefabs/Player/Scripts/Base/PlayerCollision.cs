using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Collider2D collider;

    [SerializeField] public bool isFalling { get; private set; }
    [SerializeField] public bool isOnSurface { get; private set; }
    [SerializeField] public float groundCheckDistance = 0.25f;
    [SerializeField] public float wallCheckDistance = 0.6f;
    public Vector2 faceDirection { get; private set; }

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        faceDirection = Vector2.right;
    }

    private void Update()
    {
        HandleDetectGround();
        HandleDetectFalling();
    }
    public bool isDetectedWallOnX(float direction)
    {
        return Utils.isDetectedWallOnX(collider, direction, wallCheckDistance, LayerMask.GetMask(Constants.LayerMask.Ground));
    }

    public void FlipX()
    {
        faceDirection *= -1;
        Utils.FlipX(gameObject);
    }

    public bool isOnWall
    {
        get { return isDetectedWallOnX(faceDirection.x) && !isOnSurface; }
    }

    void HandleDetectGround()
    {
        isOnSurface = Utils.isDetectedGround(collider, rigidbody2D.velocity, groundCheckDistance, LayerMask.GetMask(Constants.LayerMask.Ground));
    }

    void HandleDetectFalling()
    {
        float checkPoint = 0.01f;
        isFalling = rigidbody2D.velocity.y < -checkPoint;
    }

    void OnDrawGizmos()
    {
        Utils.DrawDetectedWallGizmos(collider, wallCheckDistance, isDetectedWallOnX(faceDirection.x), faceDirection.x);
    }
}
