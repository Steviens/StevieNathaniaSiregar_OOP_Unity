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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
        float inputX = 0f;
        float inputY = 0f;

        if (Input.GetKey(KeyCode.W)) inputY = 1f;
        if (Input.GetKey(KeyCode.S)) inputY = -1f;
        if (Input.GetKey(KeyCode.D)) inputX = 1f;
        if (Input.GetKey(KeyCode.A)) inputX = -1f;

        Debug.Log("Input X: " + inputX + ", Input Y: " + inputY);

        moveDirection = new Vector2(inputX, inputY).normalized;
        rb.velocity = moveDirection * maxSpeed.x;
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
