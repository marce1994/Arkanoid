using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private static CollisionManager instance;
    private List<CustomCollider2D> customSphereColliders2Ds;

    public bool collideSameLayer;

    public static CollisionManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<CollisionManager>();
            if (instance == null)
            {
                GameObject container = new GameObject("CollisionManager");
                instance = container.AddComponent<CollisionManager>();
            }
        }

        return instance;
    }

    public void RegisterCollider(CustomCollider2D collider)
    {
        if (customSphereColliders2Ds == null)
            customSphereColliders2Ds = new List<CustomCollider2D>();

        if (customSphereColliders2Ds.Contains(collider))
            return;

        customSphereColliders2Ds.Add(collider);
    }

    public void UnregisterCollider(CustomCollider2D collider)
    {
        if (customSphereColliders2Ds == null)
            return;

        customSphereColliders2Ds.Remove(collider);
    }

    private void Update()
    {
        if (customSphereColliders2Ds == null) return;

        foreach (var item in customSphereColliders2Ds)
            item.CheckCollision(customSphereColliders2Ds);
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}
