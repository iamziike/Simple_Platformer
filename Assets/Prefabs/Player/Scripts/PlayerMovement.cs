using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidbody2D;

    [SerializeField] float speed = 12;
    [SerializeField] public bool isOnSurface { get; private set; }

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 5f;
    }

    private void Update()
    {
        float checkPoint = 0.01f;
        isOnSurface = rigidbody2D.velocity.y < checkPoint && rigidbody2D.velocity.y > -checkPoint;
    }

    public void ApplyHorizontalMovement(float x)
    {
        rigidbody2D.velocity = new Vector2(x * speed, rigidbody2D.velocity.y);
    }

    public void ApplyVerticalMovement(float y)
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, y);
    }
}
