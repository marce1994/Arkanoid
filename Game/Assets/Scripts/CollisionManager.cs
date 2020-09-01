using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private static CollisionManager collisionManager;
    private List<CustomCollider2D> customCollider2Ds;

    public bool collideSameLayer;

    public CollisionManager GetInstance() {
        if (collisionManager == null)
            collisionManager = this;
        else
            Destroy(this.gameObject);
        return collisionManager;
    }

    public void RegisterCollider(CustomCollider2D customCollider2D) {
        if (customCollider2Ds == null)
            customCollider2Ds = new List<CustomCollider2D>();
        
        if (customCollider2Ds.Contains(customCollider2D))
            return;

        customCollider2Ds.Add(customCollider2D);
    }

    public void UnregisterCollider(CustomCollider2D customCollider2D) {
        if (customCollider2Ds == null)
            return;

        customCollider2Ds.Remove(customCollider2D);
    }

    private void FixedUpdate()
    {
        
    }
}
