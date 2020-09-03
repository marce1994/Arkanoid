using System;
using System.Collections.Generic;
using UnityEngine;
public enum ColliderType
{
    Sphere,
    AABB
}

public class CustomCollider2D : MonoBehaviour
{
    private List<CustomCollider2D> customCollider2DsCollissionning;

    public ColliderType ColliderType;

    public event Action<CustomCollision> onCollisionEnter2D;
    public event Action<CustomCollision> onCollisionExit2D;

    public float width = 0.0f;
    public float heigth = 0.0f;

    public float radius = 0.0f;

    public Vector3 centerOffset;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        switch (ColliderType)
        {
            case ColliderType.Sphere:
                {
                    Gizmos.DrawWireSphere(transform.position + centerOffset, radius);
                }
                break;
            case ColliderType.AABB:
                {
                    Gizmos.DrawWireCube(transform.position + centerOffset, new Vector3(width, heigth, 0));
                }
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        CollisionManager.GetInstance().RegisterCollider(this);

        onCollisionEnter2D += (CustomCollision col) =>
        {
            Debug.Log("CollisionEnter!");
        };

        onCollisionExit2D += (CustomCollision col) =>
        {
            Debug.Log("CollisionExit!");
        };
    }

    private void OnDestroy()
    {
        CollisionManager.GetInstance().UnregisterCollider(this);
        onCollisionExit2D = null;
        onCollisionEnter2D = null;
    }

    public void CalculateCollisions(IEnumerable<CustomCollider2D> colliders)
    {
        foreach (var collider in colliders)
        {
            if (collider == this) continue;
            switch (ColliderType)
            {
                case ColliderType.Sphere:
                    {
                        switch (collider.ColliderType)
                        {
                            case ColliderType.Sphere:
                                {
                                    if (CircleCollision(this.transform.position + this.centerOffset, this.radius, collider.transform.position + collider.centerOffset, collider.radius))
                                    {
                                        if (AddCollider(collider))
                                            onCollisionEnter2D(new CustomCollision(collider.transform.position + collider.centerOffset, collider));
                                    }
                                    else if (RemoveCollider(collider))
                                        onCollisionExit2D(new CustomCollision(collider.transform.position + collider.centerOffset, collider));
                                }
                                break;
                            case ColliderType.AABB:
                                {
                                    if (CircleAABBCollision(this.transform.position + this.centerOffset, this.radius, collider.transform.position + collider.centerOffset, new Vector2(collider.width, collider.heigth)))
                                    {
                                        if (AddCollider(collider))
                                            onCollisionEnter2D(new CustomCollision(GetCollisionPoint(this, collider), collider));
                                    }
                                    else if (RemoveCollider(collider))
                                        onCollisionExit2D(new CustomCollision(GetCollisionPoint(this, collider), collider));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case ColliderType.AABB:
                    {
                        switch (collider.ColliderType)
                        {
                            case ColliderType.Sphere:
                                {
                                    if (CircleAABBCollision(collider.transform.position + collider.centerOffset, collider.radius, this.transform.position + this.centerOffset, new Vector2(width, heigth)))
                                    {
                                        if (AddCollider(collider))
                                            onCollisionEnter2D(new CustomCollision(GetCollisionPoint(collider, this), collider));
                                    }
                                    else if (RemoveCollider(collider))
                                        onCollisionExit2D(new CustomCollision(GetCollisionPoint(collider, this), collider));
                                }
                                break;
                            case ColliderType.AABB:
                                {
                                    if (AABBCollision(transform.position + centerOffset, new Vector2(width, heigth), collider.transform.position + collider.centerOffset, new Vector2(collider.width, collider.heigth)))
                                    {
                                        if (AddCollider(collider))
                                            onCollisionEnter2D(new CustomCollision(collider.transform.position + collider.centerOffset, collider));
                                    }
                                    else if (RemoveCollider(collider))
                                        onCollisionExit2D(new CustomCollision(collider.transform.position + collider.centerOffset, collider));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private bool AddCollider(CustomCollider2D customCollider2D)
    {
        if (customCollider2DsCollissionning == null)
            customCollider2DsCollissionning = new List<CustomCollider2D>();

        if (customCollider2DsCollissionning.Contains(customCollider2D))
            return false;
        customCollider2DsCollissionning.Add(customCollider2D);
        return true;
    }

    private bool RemoveCollider(CustomCollider2D customCollider2D)
    {
        if (customCollider2DsCollissionning == null)
            customCollider2DsCollissionning = new List<CustomCollider2D>();

        if (!customCollider2DsCollissionning.Contains(customCollider2D))
            return false;
        customCollider2DsCollissionning.Remove(customCollider2D);
        return true;
    }

    private bool CircleCollision(Vector2 c1Pos, float c1Radius, Vector2 c2Pos, float c2Radius)
    {
        float distance = Mathf.Sqrt((c1Pos.x - c2Pos.x) * (c1Pos.x - c2Pos.x) + ((c1Pos.y - c2Pos.y) * (c1Pos.y - c2Pos.y)));
        return distance < c1Radius + c2Radius;
    }

    private bool AABBCollision(Vector2 s1Pos, Vector2 s1Size, Vector2 s2Pos, Vector2 s2Size)
    {
        s1Pos = s1Pos - new Vector2(s1Size.x / 2, s1Size.y / 2);
        s2Pos = s2Pos - new Vector2(s2Size.x / 2, s2Size.y / 2);

        return (s1Pos.x < s2Pos.x + s2Size.x &&
                s1Pos.x + s1Size.x > s2Pos.x &&
                s1Pos.y < s2Pos.y + s2Size.y &&
                s1Pos.y + s1Size.y > s2Pos.y);
    }

    private bool CircleAABBCollision(Vector2 circlePosition, float circleRadius, Vector2 aabbPosition, Vector2 aabbSize)
    {
        var distance = new Vector2(Math.Abs(circlePosition.x - aabbPosition.x), Math.Abs(circlePosition.y - aabbPosition.y));

        if (distance.x > (aabbSize.x / 2 + circleRadius)) { return false; }
        if (distance.y > (aabbSize.y / 2 + circleRadius)) { return false; }

        if (distance.x <= (aabbSize.y / 2)) { return true; }
        if (distance.y <= (aabbSize.x / 2)) { return true; }

        var cornerDistance_sq = Math.Pow((distance.x - aabbPosition.x / 2), 2) + Math.Pow((distance.y - aabbPosition.y / 2), 2);

        return (cornerDistance_sq <= (Math.Pow(circleRadius, 2)));
    }

    private Vector3 GetCollisionPoint(CustomCollider2D col1, CustomCollider2D col2)
    {
        //Chequeo circulo con cuadrado.... por ahora => arreglar.
        var dirVector =  (col2.transform.position + col2.centerOffset) - (col1.transform.position + col1.centerOffset);
        var closestPoint = dirVector.normalized * col1.radius;

        return col1.transform.position + closestPoint;
    }
}
