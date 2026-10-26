using System.Collections.Generic;
using UnityEngine;

// Object Pool pattern: bullets are created once and reused.
public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int initialSize = 20;
    private readonly Queue<Bullet> available = new Queue<Bullet>();

    void Awake()
    {
        for (int i = 0; i < initialSize; i++)
            available.Enqueue(CreateBullet());
    }

    private Bullet CreateBullet()
    {
        Bullet b = Instantiate(bulletPrefab, transform);
        b.Init(this);
        b.gameObject.SetActive(false);
        return b;
    }

    public Bullet Get(Vector3 position)
    {
        Bullet b = available.Count > 0 ? available.Dequeue() : CreateBullet(); // grows if empty
        b.transform.position = position;
        b.gameObject.SetActive(true);
        return b;
    }

    public void Return(Bullet b)
    {
        if (!b.gameObject.activeSelf) return; // avoid returning twice
        b.gameObject.SetActive(false);
        available.Enqueue(b);
    }
}