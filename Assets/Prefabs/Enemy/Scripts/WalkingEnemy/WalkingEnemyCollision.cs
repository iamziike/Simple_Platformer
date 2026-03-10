using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyCollision : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Collider2D collider;
    [SerializeField] public float wallCheckDistance = 0.85f;
    [SerializeField] public float groundCheckDistance = 0.45f;
    [SerializeField] Vector2 edgeCheckDistance = new Vector2(0f, 2.5f);
    public bool isDetectedWall { get; private set; }
    public bool isOnSurface { get; private set; }
    public bool isReachedEdge { get; private set; }
    public bool isOnEdge { get; private set; }
    public Vector2 faceDirection { get; private set; }

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        faceDirection = Vector2.left;
    }

    private void Update()
    {
        HandleDetectWall();
        HandleDetectGround();
        HandleDetectEdge();
    }

    public void FlipX()
    {
        faceDirection *= -1;
        Utils.FlipX(gameObject);
    }

    void HandleDetectGround()
    {
        isOnSurface = Utils.isDetectedGround(collider, rigidbody2D.velocity, groundCheckDistance, LayerMask.GetMask(Constants.LayerMask.Ground));
    }

    void HandleDetectEdge()
    {
        isReachedEdge = Utils.isDetectedNoGroundEdge(collider, rigidbody2D.velocity, edgeCheckDistance, LayerMask.GetMask(Constants.LayerMask.Ground));
    }

    void HandleDetectWall()
    {
        isDetectedWall = Utils.isDetectedWallOnX(collider, faceDirection.x, wallCheckDistance, LayerMask.GetMask(Constants.LayerMask.Ground));
    }

    void OnDrawGizmos()
    {
        // Utils.DrawDetectedWallGizmos(transform, faceDirection.x, wallCheckDistance, isDetectedWall);
        Utils.DrawDetectedGroundEdgeGizmos(collider, edgeCheckDistance, isReachedEdge);
        // Utils.DrawDetectedGroundGizmos(collider, groundCheckDistance, isOnSurface);
    }
}
