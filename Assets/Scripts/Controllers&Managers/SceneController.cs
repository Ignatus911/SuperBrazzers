using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public bool isDeathScreen()
    {
        return SceneManager.GetActiveScene().name == "DeathScene";
    }

    public void GameOver()
    {
        // Убить все скрипты, загрузить стартовый экран
    }

    public void LoadNextLVL()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadDeathScreen()
    {
        SceneManager.LoadScene("DeathScene");
        StartCoroutine(DeathSceneTime());
    }


    private IEnumerator DeathSceneTime()
    {
        Debug.Log("poshlo 5 secund");
        yield return new WaitForSecondsRealtime(3);
        Debug.Log("proshlo 5 secund");
        SceneManager.LoadScene("World1-1");
    }
}
