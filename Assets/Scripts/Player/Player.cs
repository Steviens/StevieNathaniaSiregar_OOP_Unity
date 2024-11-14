using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private PlayerMovement playerMovement;
    private Animator animator;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Dapatkan komponen PlayerMovement
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("Komponen PlayerMovement tidak ditemukan pada GameObject Player.");
        }

        // Dapatkan komponen Animator dari EngineEffects
        GameObject engineEffects = GameObject.Find("EngineEffects");
        if (engineEffects != null)
        {
            animator = engineEffects.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Komponen Animator tidak ditemukan pada GameObject EngineEffects.");
            }
        }
        else
        {
            Debug.LogError("GameObject EngineEffects tidak ditemukan di scene.");
        }
    }

    void FixedUpdate()
    {
        if (playerMovement != null)
        {
            playerMovement.Move();
        }
    }

    void LateUpdate()
    {
        if (playerMovement != null)
        {
            playerMovement.MoveBound();
        }

        if (animator != null && playerMovement != null)
        {
            animator.SetBool("IsMoving", playerMovement.IsMoving());
        }
    }

    private WeaponPickup currentWeaponPickup;

    public void SwitchWeapon(Weapon newWeapon, WeaponPickup newWeaponPickup)
    {
        if (currentWeaponPickup != null)
        {
            currentWeaponPickup.PickupHandler(true);  // Membuat pickup senjata sebelumnya muncul kembali
        }
        currentWeaponPickup = newWeaponPickup;
    }
}
