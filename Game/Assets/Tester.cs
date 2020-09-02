using UnityEngine;

public class Tester : MonoBehaviour
{
    public GameObject c1;
    public float c1Radius;
    public GameObject c2;
    public float c2Radius;

    public GameObject s1;
    public Vector2 s1Size;
    public GameObject s2;
    public Vector2 s2Size;

    void Update()
    {
        if (!Algebra.Collision.CircleCollision(c1.transform.position, c1Radius, c2.transform.position, c2Radius))
        {
            c1.transform.position += Vector3.right * 10.0f * Time.deltaTime;
        }

        if (!Algebra.Collision.AABBCollision(s1.transform.position, s1Size, s2.transform.position, s2Size))
        {
            s1.transform.position += Vector3.right * 10.0f * Time.deltaTime;
        }
    }
}
