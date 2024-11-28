using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private Vector2 maxSpeed = new Vector2(7, 5); // Kecepatan maksimum untuk X dan Y
    [SerializeField] private Vector2 timeToFullSpeed = new Vector2(1, 1); // Waktu untuk mencapai kecepatan maksimum
    [SerializeField] private Vector2 timeToStop = new Vector2(0.5f, 0.5f); // Waktu untuk berhenti
    [SerializeField] private Vector2 stopClamp = new Vector2(2.5f, 2.5f); // Batas kecepatan minimum sebelum berhenti

    private Vector2 moveDirection;
    private float moveVelocityX;
    private float moveVelocityY;
    private float moveFrictionX;
    private float moveFrictionY;
    private float stopFrictionX;
    private float stopFrictionY;

    // Fields for boundary constraints
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        // Mengambil komponen Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Menambahkan pembekuan rotasi pada Rigidbody2D
        rb.freezeRotation = true;

        // Menghitung nilai-nilai awal untuk akselerasi dan gesekan
        moveVelocityX = (2 * maxSpeed.x) / timeToFullSpeed.x;
        moveVelocityY = (2 * maxSpeed.y) / timeToFullSpeed.y;
        moveFrictionX = (-2 * maxSpeed.x) / Mathf.Pow(timeToFullSpeed.x, 2);
        moveFrictionY = (-2 * maxSpeed.y) / Mathf.Pow(timeToFullSpeed.y, 2);
        stopFrictionX = (-2 * maxSpeed.x) / Mathf.Pow(timeToStop.x, 2);
        stopFrictionY = (-2 * maxSpeed.y) / Mathf.Pow(timeToStop.y, 2);

        // Set screen bounds untuk pengujian
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
        // Debug log untuk memastikan nilai screenBounds
        Debug.Log("Screen Bounds: " + screenBounds);

        // Cek dimensi objek menggunakan SpriteRenderer atau BoxCollider2D sebagai alternatif
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            objectWidth = spriteRenderer.bounds.extents.x;
            objectHeight = spriteRenderer.bounds.extents.y;
        }
        else
        {
            var boxCollider = GetComponent<BoxCollider2D>();
            if (boxCollider != null)
            {
                objectWidth = boxCollider.bounds.extents.x;
                objectHeight = boxCollider.bounds.extents.y;
            }
            else
            {
                Debug.LogWarning("No SpriteRenderer or BoxCollider2D found on Player. Please add one of these components.");
            }
        }
        
        // Debug log untuk memeriksa dimensi objek
        Debug.Log("Object Width: " + objectWidth + ", Object Height: " + objectHeight);
    }

    void FixedUpdate()
    {
        Move(); // Memanggil fungsi Move setiap frame fisik untuk menggerakkan Player
        ConstrainToScreen(); // Batasi pergerakan pesawat agar tidak keluar dari batas layar
    }

    public void Move()
    {
        // Mengambil input langsung dari tombol keyboard
        float inputX = 0f;
        float inputY = 0f;

        if (Input.GetKey(KeyCode.W)) inputY = 1f; // Tombol W untuk ke atas
        if (Input.GetKey(KeyCode.S)) inputY = -1f; // Tombol S untuk ke bawah
        if (Input.GetKey(KeyCode.D)) inputX = 1f; // Tombol D untuk ke kanan
        if (Input.GetKey(KeyCode.A)) inputX = -1f; // Tombol A untuk ke kiri

        // Menentukan arah dan kecepatan
        moveDirection = new Vector2(inputX, inputY).normalized;

        // Memperbarui kecepatan langsung berdasarkan arah dan kecepatan maksimum
        rb.velocity = moveDirection * maxSpeed.x;
    }

    private void ConstrainToScreen()
    {
        // Membatasi posisi pemain di dalam batas layar
        Vector3 pos = transform.position;
        
        // Menggunakan nilai batas yang disesuaikan
        pos.x = Mathf.Clamp(pos.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);
        
        transform.position = pos; // Memperbarui posisi dengan batasan yang diterapkan
    }

    private Vector2 GetFriction()
    {
        // Mengembalikan nilai gesekan berdasarkan status gerakan
        return moveDirection != Vector2.zero ? 
            new Vector2(moveFrictionX, moveFrictionY) : 
            new Vector2(stopFrictionX, stopFrictionY);
    }

    public bool IsMoving()
    {
        // Mengembalikan true jika pemain bergerak, jika tidak maka false
        return rb.velocity.magnitude > stopClamp.magnitude;
    }
}