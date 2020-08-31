using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ColliderType
{
    Sphere,
    AABB
}

public class CustomCollider2D : MonoBehaviour
{
    public ColliderType ColliderType;
    public event Action onCollisionEnter;

    private void FixedUpdate()
    {
        // Physics
    }
}
