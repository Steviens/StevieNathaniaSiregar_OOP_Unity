using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Jika Bullet bertabrakan dengan objek lain, lakukan sesuatu, misalnya menimbulkan damage.
        // Contoh sederhana:
        if (other.CompareTag("Enemy"))
        {
            // Menghancurkan musuh atau memberikan damage
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        // Ketika bullet keluar dari layar, hancurkan objek ini atau kembalikan ke pool
        Destroy(gameObject);
    }
}
