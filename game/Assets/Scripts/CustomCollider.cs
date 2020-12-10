using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class CustomCollider2D : MonoBehaviour
{
    private List<CustomCollider2D> customCollider2DsCollissionning;

    public event Action<CustomCollision> OnCollisionEnter2D;
    public event Action<CustomCollision> OnCollisionExit2D;

    public Vector3 Offset;
    public Vector3 PrevCenter;

    public Vector3 Center
    {
        get
        {
            return transform.position + Offset;
        }
    }

    public Vector3 PreviousCenter
    {
        get
        {
            return PrevCenter + Offset;
        }
    }

    public virtual void CheckCollision(List<CustomCollider2D> colliders)
    {

        foreach (var item in colliders)
        {
            if (item.transform.position == this.transform.position) continue;

            if (item is CustomSphereCollider2D)
            {
                var res = Collide(item as CustomSphereCollider2D);
                if (res.Item1)
                {
                    if (AddCollider(item))
                        OnCollisionEnter2D(res.Item2);
                }
                else
                {
                    if (RemoveCollider(item))
                        OnCollisionExit2D(res.Item2);
                }
            }

            if (item is CustomBoxCollider2D)
            {
                var res = Collide(item as CustomBoxCollider2D);
                if (res.Item1)
                {
                    if (AddCollider(item))
                        OnCollisionEnter2D(res.Item2);

                }
                else
                {
                    if (RemoveCollider(item))
                        OnCollisionExit2D(res.Item2);
                }
            }
        }
    }

    public virtual bool AddCollider(CustomCollider2D customCollider2D)
    {
        if (customCollider2DsCollissionning == null)
            customCollider2DsCollissionning = new List<CustomCollider2D>();

        if (customCollider2DsCollissionning.Contains(customCollider2D))
            return false;
        customCollider2DsCollissionning.Add(customCollider2D);
        return true;
    }

    public virtual bool RemoveCollider(CustomCollider2D customCollider2D)
    {
        if (customCollider2DsCollissionning == null)
            customCollider2DsCollissionning = new List<CustomCollider2D>();

        if (!customCollider2DsCollissionning.Contains(customCollider2D))
            return false;
        customCollider2DsCollissionning.Remove(customCollider2D);
        return true;
    }

    public virtual void Awake()
    {
        CollisionManager.GetInstance().RegisterCollider(this);

        OnCollisionEnter2D += (CustomCollision col) =>
        {
            Debug.Log("CollisionEnter!");
        };

        OnCollisionExit2D += (CustomCollision col) =>
        {
            Debug.Log("CollisionExit!");
        };
    }

    public virtual void OnDestroy()
    {
        CollisionManager.GetInstance().UnregisterCollider(this);
        OnCollisionExit2D = null;
        OnCollisionEnter2D = null;
    }

    public abstract void OnDrawGizmos();
    public virtual void LateUpdate()
    {
        PrevCenter = transform.position;
    }

    public abstract Tuple<bool, CustomCollision> Collide(CustomSphereCollider2D collider);
    public abstract Tuple<bool, CustomCollision> Collide(CustomBoxCollider2D collider);
}