using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    void Awake()
    {
        if (animator != null)
        {
            animator.enabled = false;
        }
        else
        {
            Debug.LogError("Animator tidak terpasang di LevelManager. Silakan pasang Animator di Inspector.");
        }
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        if (animator != null)
        {
            animator.enabled = true;
            animator.SetTrigger("startTransition");
        }

        yield return new WaitForSeconds(1);

        // Load scene secara asinkron
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Trigger akhir transisi
        if (animator != null)
        {
            animator.SetTrigger("endTransition");
        }

        // Memastikan Player.Instance ada sebelum mencoba mengatur posisi
        if (Player.Instance != null)
        {
            Player.Instance.transform.position = new Vector3(0, -4.5f, 0);
        }
        else
        {
            Debug.LogWarning("Instance Player tidak ditemukan saat mengatur posisi setelah load scene.");
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}
