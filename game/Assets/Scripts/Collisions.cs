using System;
using UnityEngine;

public static class Collision
{
    // EXAMEN: Colision entre circulos
    public static Tuple<bool, CustomCollision> CircleCollision(CustomSphereCollider2D circle1, CustomSphereCollider2D circle2)
    {
        float distance = Mathf.Sqrt((circle1.Center.x - circle2.Center.x) * (circle1.Center.x - circle2.Center.x) + ((circle1.Center.y - circle2.Center.y) * (circle1.Center.y - circle2.Center.y)));

        var collided = distance < circle1.Radius + circle2.Radius;
        Vector3 dirVector = circle2.Center - circle1.Center;
        Vector3 closestPoint = dirVector.normalized * circle1.Radius;

        CustomCollision customCollision = new CustomCollision(circle1.Center + closestPoint, circle2, (circle1.Center - circle2.Center).normalized);
        return new Tuple<bool, CustomCollision>(collided, customCollision);
    }

    // EXAMEN: Colision entre AABB
    public static Tuple<bool, CustomCollision> AABBCollision(CustomBoxCollider2D aabb1, CustomBoxCollider2D aabb2)
    {
        Vector3 aabb1Position = aabb1.Center - new Vector3(aabb1.Width / 2, aabb1.Height / 2);
        Vector3 aabb2Position = aabb2.Center - new Vector3(aabb2.Width / 2, aabb2.Height / 2);

        var collided = (aabb1Position.x < aabb2Position.x + aabb2.Width &&
                        aabb1Position.x + aabb1.Width > aabb2Position.x &&
                        aabb1Position.y < aabb2Position.y + aabb2.Height &&
                        aabb1Position.y + aabb1.Height > aabb2Position.y);
        CustomCollision customCollision = new CustomCollision(Vector3.zero, aabb2);

        return new Tuple<bool, CustomCollision>(collided, customCollision);
    }

    // EXAMEN: Colision entre AABB y circulo + Normal
    public static Tuple<bool, CustomCollision> CircleAABBCollision(CustomSphereCollider2D circle, CustomBoxCollider2D aabb)
    {
        Vector3 dir = aabb.Center - circle.Center;
        dir = dir.normalized;
        dir *= circle.Radius;
        Vector3 pos = circle.Center + dir;

        var aabbCenter = aabb.Center - new Vector3(aabb.Width / 2, aabb.Height / 2);
        var collided = (aabbCenter.x < pos.x &&
                aabbCenter.x + aabb.Width > pos.x &&
                aabbCenter.y < pos.y &&
                aabbCenter.y + aabb.Height > pos.y);

        Vector3 normal = Vector3.zero;

        Vector3 max = aabb.Center + new Vector3(aabb.Width / 2, aabb.Height / 2);
        Vector3 min = aabb.Center + new Vector3(-aabb.Width / 2, -aabb.Height / 2);

        if (max.x > circle.PreviousCenter.x && min.x < circle.PreviousCenter.x)
            if (circle.PreviousCenter.x > aabb.Center.x)
                normal = Vector2.up;
            else
                normal = Vector2.down;

        if (max.y > circle.PreviousCenter.y && min.y < circle.PreviousCenter.y)
            if (circle.PreviousCenter.y > aabb.Center.y)
                normal = Vector3.left;
            else
                normal = Vector3.right;

        Vector3 dirVector = aabb.Center - circle.PreviousCenter;
        Vector3 closestPoint = dirVector.normalized * circle.Radius;
        Vector3 collisionPoint = circle.PreviousCenter + closestPoint;

        CustomCollision customCollision = new CustomCollision(circle.PreviousCenter + closestPoint, aabb, normal == Vector3.zero ? circle.PreviousCenter - collisionPoint : normal);
        return new Tuple<bool, CustomCollision>(collided, customCollision);
    }
}