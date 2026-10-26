using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifetime = 2f;
    private float timer;
    private BulletPool pool;

    public void Init(BulletPool owner)
    {
        pool = owner;
    }

    void OnEnable()
    {
        timer = lifetime;
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0f) pool.Return(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!gameObject.activeSelf) return; // already returned to the pool

        if (other.TryGetComponent<Target>(out var target))
        {
            target.Hit();
            pool.Return(this);
        }
    }
}
