using System;
using UnityEngine;

public class CustomBoxCollider2D : CustomCollider2D
{
    public float Width, Height = 1f;

    public override Tuple<bool, CustomCollision> Collide(CustomSphereCollider2D collider)
    {
        return Collision.CircleAABBCollision(collider, this);
    }

    public override Tuple<bool, CustomCollision> Collide(CustomBoxCollider2D collider)
    {
        return Collision.AABBCollision(this, collider);
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Center, new Vector3(Width, Height, 0));
    }
}