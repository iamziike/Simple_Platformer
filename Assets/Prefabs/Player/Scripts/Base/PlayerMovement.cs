using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public Vector2 velocity => rigidbody2D.velocity;


    [Header("Player Stats")]
    [SerializeField] public readonly float speed = 12;

    public bool isMoving => Utils.ChangeToBoolValue(velocity.x);

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 5f;
    }

    public void MoveTowards(Vector2 movement)
    {
        rigidbody2D.velocity = new Vector2(movement.x, movement.y);
    }

    public void Run(Vector2 movement)
    {
        rigidbody2D.velocity = new Vector2(movement.x * speed, movement.y);
    }
}
