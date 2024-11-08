using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private Vector2 maxSpeed = new Vector2(7, 5);
    [SerializeField] private Vector2 timeToFullSpeed = new Vector2(1, 1);
    [SerializeField] private Vector2 timeToStop = new Vector2(0.5f, 0.5f);
    [SerializeField] private Vector2 stopClamp = new Vector2(2.5f, 2.5f);

    private Vector2 moveDirection;
    private float moveVelocityX, moveVelocityY;
    private float moveFrictionX, moveFrictionY;
    private float stopFrictionX, stopFrictionY;

    // Batas layar
    private float leftBound;
    private float rightBound;
    private float topBound;
    private float bottomBound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Mendapatkan batas layar berdasarkan posisi kamera utama
        leftBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        rightBound = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        bottomBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        topBound = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        // Hitung kecepatan dan friksi untuk axis X dan Y
        moveVelocityX = (2 * maxSpeed.x) / timeToFullSpeed.x;
        moveVelocityY = (2 * maxSpeed.y) / timeToFullSpeed.y;
        moveFrictionX = (-2 * maxSpeed.x) / Mathf.Pow(timeToFullSpeed.x, 2);
        moveFrictionY = (-2 * maxSpeed.y) / Mathf.Pow(timeToFullSpeed.y, 2);
        stopFrictionX = (-2 * maxSpeed.x) / Mathf.Pow(timeToStop.x, 2);
        stopFrictionY = (-2 * maxSpeed.y) / Mathf.Pow(timeToStop.y, 2);
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        // Input gerakan untuk sumbu X dan Y
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(inputX, inputY).normalized;
        rb.velocity = moveDirection * maxSpeed;

        // Membatasi pergerakan di dalam batas layar
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, leftBound, rightBound);
        position.y = Mathf.Clamp(position.y, bottomBound, topBound);
        transform.position = position;
    }

    private Vector2 GetFriction()
    {
        return moveDirection != Vector2.zero ? 
            new Vector2(moveFrictionX, moveFrictionY) : 
            new Vector2(stopFrictionX, stopFrictionY);
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > stopClamp.magnitude;
    }
}
