using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool isJumpPressed = false;
    Vector2 _direction = Vector2.zero;

    public Vector2 direction
    {
        get
        {
            return _direction;
        }

        private set
        {
            _direction = value;
        }
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        isJumpPressed = Input.GetKeyDown(KeyCode.Space);
        direction = new Vector2(x, y);
    }
}
