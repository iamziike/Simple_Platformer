using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 3;
    public float currentSpeed { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(float direction = -1)
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        currentSpeed = rb.velocity.x;
    }
}
