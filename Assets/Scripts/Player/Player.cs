using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance{
        get; set; 
    }
    private PlayerMovement playerMovement;
    private Animator animator;
    public Weapon thisweapon;

    void Awake()
    {
        if (Instance !=null && Instance!= this){
            Destroy(gameObject);
        }else{
            Instance=this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        // Mengambil komponen PlayerMovement dan Animator
        playerMovement = GetComponent<PlayerMovement>();

        // Pastikan GameObject "EngineEffect" memiliki komponen Animator
        animator = GameObject.Find("EngineEffect")?.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Memanggil fungsi Move untuk menggerakkan pemain
        playerMovement.Move();
    }

    void LateUpdate()
    {
        // Mengatur parameter animasi berdasarkan status pergerakan
        if (animator != null)
        {
            animator.SetBool("IsMoving", playerMovement.IsMoving());
        }
    }
}