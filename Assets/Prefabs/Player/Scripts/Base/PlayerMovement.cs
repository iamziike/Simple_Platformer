using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public Vector2 velocity => rigidbody2D.velocity;
    [SerializeField] public readonly float speed = 12;

    [Header("KnockBack State")]
    [SerializeField] public float knockBackForce = 15f;

    public bool isMoving => Utils.ChangeToBoolValue(velocity.x);

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 5f;
    }

    public Vector2 verboseNormalizedVelocity => new Vector2(velocity.x != 0 ? velocity.x / Mathf.Abs(velocity.x) : 0, velocity.y != 0 ? velocity.y / Mathf.Abs(velocity.y) : 0);

    public void MoveTowards(Vector2 movement)
    {
        rigidbody2D.velocity = new Vector2(movement.x, movement.y);
    }

    public void Run(Vector2 movement)
    {
        rigidbody2D.velocity = new Vector2(movement.x * speed, movement.y);
    }

    public void KnockBack(Vector2 direction)
    {
        rigidbody2D.AddForce(direction * knockBackForce, ForceMode2D.Impulse);
    }
}
