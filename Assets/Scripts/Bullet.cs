using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;

    private Rigidbody2D rb;

    public IObjectPool<Bullet> objectPool;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity =  transform.up * bulletSpeed;
    }

    public void SetPool(IObjectPool<Bullet> pool)
    {
        ObjectPool = pool;
    }

    private void FixedUpdate()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }

    private void OnBecameInvisible()
    {
        // Add condition to check if the bullet should be released
        if (gameObject.activeSelf && ObjectPool != null)
        {
            ObjectPool.Release(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var hitbox = collision.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            // Apply damage to the enemy
            hitbox.Damage(damage);
            ObjectPool.Release(this);
        }   
    }
}
