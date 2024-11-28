using UnityEngine;
using UnityEngine.SceneManagement; // Tambahkan ini untuk menggunakan SceneManager
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Awake()
    {
        if (animator == null)
        {
            animator = GameObject.Find("SceneTransition").GetComponent<Animator>();
        }

        animator.gameObject.SetActive(false);
    }

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        animator.gameObject.SetActive(true);
        // Memulai transisi menggunakan animator (opsional)
        if (animator != null)
        {
            animator.SetTrigger("Start");
            yield return new WaitForSeconds(1f); // Sesuaikan waktu transisi
        }

        // Load scene secara asynchronous
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Tunggu hingga proses loading selesai
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Akhiri transisi setelah loading selesai
        if (animator != null)
        {
            animator.ResetTrigger("Start");
            animator.SetTrigger("End");
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
}