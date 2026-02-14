using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    public Vector2 faceDirection { get; private set; }
    [SerializeField] PlayerSkin playerSkin;

    void Awake()
    {
        faceDirection = Vector2.right;
    }

    public void FlipX()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    bool isAlreadyFacingDirection(Vector2 newDirectionToFace)
    {
        return transform.localScale.x > 0 && newDirectionToFace.x > 0 || transform.localScale.x < 0 && newDirectionToFace.x < 0;
    }

    public void ApplyFaceDirection(Vector2 newDirectionToFace)
    {
        if (Utils.ChangeToBoolValue(newDirectionToFace.x))
        {
            if (!isAlreadyFacingDirection(newDirectionToFace))
            {
                FlipX();
            }

            faceDirection = newDirectionToFace;
        }
    }
}
