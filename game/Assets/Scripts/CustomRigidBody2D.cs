using UnityEngine;

public class CustomRigidBody2D : MonoBehaviour
{
    private Vector3 _direction;
    public float gravity = 1f;
    public float mass = 1f;

    public Vector3 Velocity
    {
        get { return _direction; }
        set { _direction = value; }
    }

    private void Awake()
    {
        _direction = Vector3.up;
    }

    private void Update()
    {
        _direction -= Vector3.down * Time.deltaTime * gravity;
        //_direction = _direction.normalized;
        transform.position += _direction * Time.deltaTime * mass;
    }
}
