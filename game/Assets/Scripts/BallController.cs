using UnityEngine;

public class BallController : MonoBehaviour
{
    private new CustomCollider2D collider;
    private new Transform transform;
    private new CustomRigidBody2D rigidbody;

    private Vector3 lastCollisionPoint;
    private float difficultyMultiplier;

    public AudioClip racket_col_audioclip;

    public float racketHitFactor = 1f;
    public float blockHitFactor = .8f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(lastCollisionPoint, 1);
        if(transform != null)
            Gizmos.DrawLine(transform.position, transform.position + rigidbody.Velocity * 15);
    }

    private void Awake()
    {
        collider = GetComponent<CustomCollider2D>();
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<CustomRigidBody2D>();

        difficultyMultiplier = 1f;
    }

    void Start()
    {
        collider.OnCollisionEnter2D += onCollisionEnter;
    }

    private void Update()
    {
        if(difficultyMultiplier < 5)
            difficultyMultiplier += Time.deltaTime / 10;
    }

    private void onCollisionEnter(CustomCollision col)
    {
        AudioSource.PlayClipAtPoint(racket_col_audioclip, Camera.main.transform.position, 1);

        float x = 0;

        if (col.collider is CustomBoxCollider2D)
            x = hitFactor(transform.position, col.collider.transform.position, (col.collider as CustomBoxCollider2D).Width);

        Bounce(col.normal);

        Vector2 dir = rigidbody.Velocity;

        if (col.collider.gameObject.name.Contains("racket"))
            dir = new Vector2(x * racketHitFactor, rigidbody.Velocity.y).normalized;
        if (col.collider.gameObject.name.Contains("block"))
            dir = new Vector2(x * blockHitFactor, rigidbody.Velocity.y).normalized;

        rigidbody.Velocity = dir.normalized;
    }

    private float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        return (ballPos.x - racketPos.x) / racketWidth;
    }

    private void Bounce(Vector3 collisionNormal)
    {
        float speed = rigidbody.Velocity.magnitude;
        Vector3 direction = Vector3.Reflect(rigidbody.Velocity.normalized, collisionNormal).normalized;
        rigidbody.Velocity = direction * speed;
    }

    private void OnBecameInvisible()
    {
        transform.position = Vector3.zero;
        difficultyMultiplier = 1f;
        rigidbody.Velocity = Vector3.up + Vector3.left;
        GameManager.GetInstance().LossLife();
    }
}