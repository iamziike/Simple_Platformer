using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool isJumpPressed = false;
    public int horizontalDirection { get; private set; }
    public int verticalDirection { get; private set; }

    void Update()
    {
        isJumpPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1);
        horizontalDirection = (int)Input.GetAxisRaw("Horizontal");
        verticalDirection = (int)Input.GetAxisRaw("Vertical");
    }
}
