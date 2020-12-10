using UnityEngine;

public class CustomRigidBody2D : MonoBehaviour
{
    private Vector3 _direction;
    public float gravity = 1f;
    public float force_scaler = 100f;

    public Vector3 Velocity
    {
        get { return _direction; }
        set { _direction = value; }
    }

    private void Awake()
    {
        _direction = Vector3.up;
    }

    // EXAMEN: Gravedad
    private void Update()
    {
        _direction -= Vector3.down * Time.deltaTime * - gravity;
        transform.position += _direction * Time.deltaTime * force_scaler;
    }
}
