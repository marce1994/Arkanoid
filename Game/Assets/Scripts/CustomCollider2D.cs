using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
public enum ColliderType
{
    Sphere,
    AABB
}

public class CustomCollider2D : MonoBehaviour
{
    public ColliderType ColliderType;
    public event Action onCollisionEnter;

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
}
