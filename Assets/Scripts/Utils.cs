using UnityEngine;

static public class Utils
{
    public static bool HasTimeElapsed(float time, float targetTime)
    {
        return targetTime > time;
    }

    public static bool ChangeToBoolValue(object value)
    {
        if (value is int intValue)
        {
            return intValue != 0;
        }
        else if (value is float floatValue)
        {
            return floatValue != 0;
        }
        else if (value is bool boolValue)
        {
            return boolValue;
        }
        return false;
    }
    public static void FlipX(GameObject gameObject)
    {
        Vector3 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;
    }

    public static bool isDetectedWallOnX(Collider2D col, float direction, float wallCheckDistance, LayerMask layerMask)
    {
        if (col == null) return false;

        Vector2 origin = col.bounds.center;
        Vector2 directionVector = new Vector2(direction, 0);
        RaycastHit2D hit = Physics2D.Raycast(origin, directionVector, wallCheckDistance, layerMask);
        return hit.collider != null;
    }

    public static bool isDetectedGround(Collider2D col, Vector2 velocity, float groundCheckDistance, LayerMask layerMask)
    {
        if (col == null) return false;

        float yVelocity = velocity.y;
        bool isNoVelocity = yVelocity < 0.01 && yVelocity > -0.01;

        Vector2 boxSize = new Vector2(col.bounds.size.x, col.bounds.size.y);
        Vector2 origin = new Vector2(col.bounds.center.x, col.bounds.min.y);

        RaycastHit2D hit = Physics2D.BoxCast(
            origin,
            boxSize,
            0f,
            Vector2.down,
            groundCheckDistance,
            layerMask
        );

        return hit.collider != null && isNoVelocity;
    }

    public static bool isDetectedNoGroundEdge(Collider2D col, Vector2 velocity, Vector2 edgeCheckDistance, LayerMask layerMask)
    {
        if (col == null) return false;

        float yVelocity = velocity.y;
        bool isNoVelocity = yVelocity < 0.01 && yVelocity > -0.01;

        Vector2 leftOrigin = new Vector2(col.bounds.min.x - edgeCheckDistance.x, col.bounds.min.y);
        Vector2 rightOrigin = new Vector2(col.bounds.max.x + edgeCheckDistance.x, col.bounds.min.y);

        RaycastHit2D leftHit = Physics2D.Raycast(leftOrigin, Vector2.down, edgeCheckDistance.y, layerMask);
        RaycastHit2D rightHit = Physics2D.Raycast(rightOrigin, Vector2.down, edgeCheckDistance.y, layerMask);

        return (leftHit.collider == null || rightHit.collider == null) && isNoVelocity;
    }

    public static void DrawDetectedGroundGizmos(Collider2D col, float groundCheckDistance, bool isOnSurface)
    {
        if (col == null) return;

        // Thin box at feet approach
        Vector2 origin = new Vector2(col.bounds.center.x, col.bounds.min.y);
        Vector2 boxSize = new Vector2(col.bounds.size.x * 0.9f, 0.05f);

        // Draw the cast end box
        Vector2 endPosition = origin + Vector2.down * groundCheckDistance;
        Gizmos.color = isOnSurface ? Color.green : Color.red;
        Gizmos.DrawWireCube(endPosition, boxSize);
    }

    public static void DrawDetectedGroundEdgeGizmos(Collider2D col, Vector2 edgeCheckDistance, bool isOnSurface)
    {
        if (col == null) return;

        Vector2 leftOrigin = new Vector2(col.bounds.min.x - edgeCheckDistance.x, col.bounds.min.y);
        Vector2 rightOrigin = new Vector2(col.bounds.max.x + edgeCheckDistance.x, col.bounds.min.y);
        Gizmos.color = isOnSurface ? Color.green : Color.red;
        Gizmos.DrawLine(leftOrigin, new Vector2(leftOrigin.x, leftOrigin.y - edgeCheckDistance.y));
        Gizmos.DrawLine(rightOrigin, new Vector2(rightOrigin.x, rightOrigin.y - edgeCheckDistance.y));
    }

    public static void DrawDetectedWallGizmos(Collider2D col, float wallCheckDistance, bool isDetectedWall, float direction)
    {
        if (col == null) return;

        Vector2 origin = col.bounds.center;
        Vector2 directionVector = new Vector2(direction, 0);
        Vector2 endPosition = origin + directionVector * wallCheckDistance;
        Gizmos.color = isDetectedWall ? Color.green : Color.red;
        Gizmos.DrawLine(origin, endPosition);
    }
}

