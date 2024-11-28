using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Singleton Instance untuk GameManager
    public static GameManager Instance { get; private set; }
    public LevelManager LevelManager { get; private set; }

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LevelManager = GetComponentInChildren<LevelManager>();
        }
        else
        {
            Destroy(gameObject);
        }

        // Menghilangkan semua objek kecuali kamera dan player
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            DontDestroyOnLoad(mainCamera.gameObject);
        }
        else
        {
            Debug.LogError("Main Camera not found.");
        }
    }
}