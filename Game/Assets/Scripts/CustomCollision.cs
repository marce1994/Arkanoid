using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCollision
{
    public readonly Vector3 collisionPoint;
    public readonly CustomCollider2D collider;

    public CustomCollision(Vector3 pCol, CustomCollider2D col)
    {
        collisionPoint = pCol;
        collider = col;
    }
}
