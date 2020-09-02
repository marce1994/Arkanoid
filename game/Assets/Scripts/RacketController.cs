using UnityEngine;

public class RacketController : MonoBehaviour
{
    public float movementSpeed = 1;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        var collider = GetComponent<CustomCollider2D>();
        collider.onCollisionEnter2D += (CustomCollision col) =>
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, 1);
        };
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();    
    }

    void HandleInputs() {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += Vector3.left * Time.deltaTime * movementSpeed;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * Time.deltaTime * movementSpeed;
        }
    }
}
