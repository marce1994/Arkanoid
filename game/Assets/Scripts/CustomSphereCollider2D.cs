using System;
using UnityEngine;

public class CustomSphereCollider2D : CustomCollider2D
{
    public float Radius;

    public override Tuple<bool, CustomCollision> Collide(CustomSphereCollider2D collider)
    {
        return Collision.CircleCollision(this, collider);
    }

    public override Tuple<bool, CustomCollision> Collide(CustomBoxCollider2D collider)
    {
        return Collision.CircleAABBCollision(this, collider);
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Center, Radius);
    }
}