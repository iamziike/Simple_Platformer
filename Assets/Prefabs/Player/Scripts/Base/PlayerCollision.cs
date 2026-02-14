using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    [SerializeField] public bool isFalling { get; private set; }
    [SerializeField] public bool isOnSurface { get; private set; }
    [SerializeField] public float wallCheckDistance = 0.8f;
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
    }

    public bool isOnWall(float direction)
    {
        return isDetectedWall(direction) && !isOnSurface;
    }

    void HandleDetectGround()
    {
        float checkPoint = 0.01f;
        isOnSurface = rigidbody2D.velocity.y < checkPoint && rigidbody2D.velocity.y > -checkPoint;
    }

    void HandleDetectFalling()
    {
        float checkPoint = 0.01f;
        isFalling = rigidbody2D.velocity.y < -checkPoint;
    }


}
