using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    //private new Rigidbody2D rigidbody;
    private new CustomCollider2D collider;
    private new Transform transform;
    private Vector3 velocity;

    private Vector3 lastCollisionPoint;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(lastCollisionPoint, 1);
        Gizmos.DrawLine(transform.position, transform.position + velocity * 15);
    }

    private void Awake()
    {
        //rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CustomCollider2D>();
        transform = GetComponent<Transform>();

        velocity = Vector3.up;
    }

    // Start is called before the first frame update
    void Start()
    {
        collider.onCollisionEnter2D += onCollisionEnter;
    }

    private void FixedUpdate()
    {
        transform.position += velocity * 50.0f * Time.deltaTime;
    }

    private void onCollisionEnter(CustomCollision col)
    {
        if (col.collider.gameObject.name == "racket")
            Debug.Log("golpea la raqueta");

        lastCollisionPoint = col.collisionPoint;
        var normal = Vector2.up;
        if (col.collider.name == "border_left")
            normal = Vector2.right;
        if (col.collider.name == "border_right")
            normal = Vector2.left;
        if (col.collider.name == "border_top")
            normal = Vector2.down;

        Bounce(normal);
    }

    private void Bounce(Vector3 collisionNormal)
    {
        var speed = velocity.magnitude;
        var direction = Vector3.Reflect(velocity.normalized, collisionNormal);

        direction += new Vector3(0.1f, 0, 0);
        direction = direction.normalized;

        Debug.Log("Out Direction: " + direction);
        Debug.DrawRay(transform.position, velocity.normalized, Color.cyan, 2f);
        Debug.DrawRay(transform.position, collisionNormal, Color.magenta, 2f);
        velocity = direction * Mathf.Max(speed, 1);
    }
}
