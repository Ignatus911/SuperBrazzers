using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip mainTheme;
    [SerializeField] AudioClip pauseSound;
    [SerializeField, Range(0.2f, 1f)] float pauseSoundTime;
    [SerializeField] List<AudioClip> musics;
    [SerializeField] List<AudioClip> sounds;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        TimeController.OnPauseGame += OnPauseGame;
        PlayMusic(mainTheme);
        SceneController.OnSceneLoaded += ProceedScene;
    }

    private void ProceedScene(string sceneName)
    {
        if (sceneName == SceneController.SCENE_NAME)
            PlayMusic(mainTheme);
    }

    private void OnPauseGame(bool timeIsStoped)
    {
        PlaySound(pauseSound);
        if (musicSource.isPlaying)
            musicSource.Pause();
        else StartCoroutine(waitWileEndPauseSound());
            
    }

    IEnumerator waitWileEndPauseSound()
    {
        yield return new WaitForSeconds(pauseSoundTime);
        musicSource.UnPause();
    }

    public void PlaySound(AudioClip clip)
    {
        foreach(var sound in sounds)
        {
            if(sound == clip)
            {
                soundSource.clip = sound;
                soundSource.Play();
                return;
            }
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        foreach(var music in musics)
        {
            if(music == clip)
            {
                musicSource.clip = music;
                musicSource.Play();
                return;
            }
        }
    }
}
