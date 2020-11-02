using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public const string DEATH_SCENE_NAME = "DeathScene";
    public const string SCENE_NAME = "World1-1";

    public static SceneController Instance { get; private set; }

    public static Action<string> OnSceneLoaded;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void LoadDeathScreen()
    {
        SceneManager.LoadScene(DEATH_SCENE_NAME);
        OnSceneLoaded?.Invoke(DEATH_SCENE_NAME);
        StartCoroutine(DeathSceneTime());
    }


    private IEnumerator DeathSceneTime()
    {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(SCENE_NAME);
        OnSceneLoaded?.Invoke(SCENE_NAME);
    }
}
