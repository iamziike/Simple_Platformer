using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _health;
    Rigidbody2D rigidbody2D;
    Collider2D collider2D;

    protected void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }

    public int health
    {
        get => _health;
        set => _health = value;
    }

    public void HandleDamageTaken()
    {
        _health--;

        if (_health <= 0)
        {
            HandleDeath();
        }
    }

    protected void HandleDeath()
    {
        collider2D.enabled = false;
        JumpAndRotate();
        Destroy(gameObject, 3f);
    }

    protected void JumpAndRotate()
    {
        // Jump
        rigidbody2D.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);

        // Rotate 
        float randomTorque = Random.Range(-10f, 10f);
        rigidbody2D.constraints = RigidbodyConstraints2D.None;
        rigidbody2D.angularDrag = 0.5f; // Adjust angular drag for smoother rotation
        rigidbody2D.gravityScale = 3f; // Increase gravity for a more dramatic fall
        rigidbody2D.AddTorque(randomTorque, ForceMode2D.Impulse);
    }
}
