using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    WalkingEnemyCollision collision;
    WalkingEnemyMovement movement;
    WalkingEnemyAnimation enemyAnimation;

    [Header("Collision")]
    bool isProcessingEdgeReached = false;

    new void Awake()
    {
        base.Awake();
        collision = GetComponent<WalkingEnemyCollision>();
        movement = GetComponent<WalkingEnemyMovement>();
        enemyAnimation = GetComponent<WalkingEnemyAnimation>();
    }

    void Update()
    {
        enemyAnimation.ApplyMovementAnimation(movement.currentSpeed);
    }

    void LateUpdate()
    {
        if (collision.isOnSurface)
        {
            movement.Move(collision.faceDirection.x);
        }

        if (collision.isDetectedWall || (collision.isReachedEdge && !isProcessingEdgeReached))
        {
            collision.FlipX();
        }

        isProcessingEdgeReached = collision.isReachedEdge;
    }

    public new void HandleDamageTaken()
    {
        base.HandleDamageTaken();
        enemyAnimation.ApplyDamageTakenAnimation();
    }
}
