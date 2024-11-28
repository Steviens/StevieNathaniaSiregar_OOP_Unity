using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f; // Tingkatkan nilai agar pergerakan terlihat
    [SerializeField] private float rotateSpeed = 100.0f; // Kecepatan rotasi
    private Vector2 newPosition; // Posisi tujuan berikutnya

    private void Start()
    {
        ChangePosition();
    }

    private void Update()
    {
        // Cek apakah pemain memiliki senjata
        if (Player.Instance != null)
        {
            bool hasWeapon = Player.Instance.thisweapon != null;
            GetComponent<Collider2D>().enabled = hasWeapon;
            GetComponent<SpriteRenderer>().enabled = hasWeapon;

            // Jika tidak memiliki senjata, hentikan semua proses
            if (!hasWeapon) return;
        }

        // Debug posisi saat ini
        Debug.Log($"Portal Position: {transform.position}, Target Position: {newPosition}");

        // Pindahkan portal menuju posisi baru
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        // Rotasi portal
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

        // Jika portal mendekati target, ubah posisi
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }
    }

    private void ChangePosition()
    {
        // Tentukan posisi acak baru dalam rentang tertentu
        float posisirandom_X = Random.Range(-4f, 4f);
        float posisirandom_Y = Random.Range(-2f, 2f);
        newPosition = new Vector2(posisirandom_X, posisirandom_Y);

        Debug.Log($"Portal: New target position is {newPosition}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (other.CompareTag("Player") && player != null && player.thisweapon != null)
        {
            Debug.Log("Portal: Player entered portal successfully!");
            GameManager.Instance.LevelManager.LoadScene("Main");
        }
        else
        {
            Debug.Log("Portal: Player hasn't got a weapon!");
        }
    }
}
