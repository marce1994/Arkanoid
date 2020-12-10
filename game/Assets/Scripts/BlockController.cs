using UnityEngine;

public class BlockController : MonoBehaviour
{
    private new CustomCollider2D collider;
    public int points = 10;

    void Start()
    {
        collider = GetComponent<CustomCollider2D>();
        collider.OnCollisionEnter2D += onCollisionEnter;
    }

    private void onCollisionEnter(CustomCollision col)
    {
        GameManager.GetInstance().AddPoints(points);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        collider.OnCollisionEnter2D -= onCollisionEnter;
    }
}
