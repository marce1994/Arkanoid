using UnityEngine;

public class RacketController : MonoBehaviour
{
    public float movementSpeed = 1;
    public AudioClip clip;

    bool rigthCollider = false;
    bool leftCollider = false;

    private void Start()
    {
        var sideColliders = GetComponentsInChildren<CustomCollider2D>();
        foreach (CustomCollider2D collider in sideColliders)
        {
            collider.OnCollisionEnter2D += onCollisionEnter2D;
            collider.OnCollisionExit2D += onCollisionExit2D;
        }
    }

    private void onCollisionEnter2D(CustomCollision col)
    {
        if (!col.collider.name.Contains("border")) return;

        if (col.collisionPoint.x > transform.position.x)
            rigthCollider = true;
        if (col.collisionPoint.x < transform.position.x)
            leftCollider = true;
    }

    private void onCollisionExit2D(CustomCollision col)
    {
        if (!col.collider.name.Contains("border")) return;

        if (col.collisionPoint.x > transform.position.x)
            rigthCollider = false;
        if (col.collisionPoint.x < transform.position.x)
            leftCollider = false;
    }

    private void Update()
    {
        HandleInputs();    
    }

    private void HandleInputs() {
        if(!leftCollider)
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                transform.position += Vector3.left * Time.deltaTime * movementSpeed;
        
        if(!rigthCollider)
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                transform.position += Vector3.right * Time.deltaTime * movementSpeed;
    }
}
