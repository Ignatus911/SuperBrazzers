using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip currentMusic;
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
        musicSource.Play();
    }

    private void OnPauseGame(bool timeIsStoped)
    {
        Play(pauseSound);
        if (musicSource.isPlaying)
            musicSource.Pause();
        else StartCoroutine(waitWileEndPauseSound());
            
    }

    IEnumerator waitWileEndPauseSound()
    {
        yield return new WaitForSeconds(pauseSoundTime);
        musicSource.UnPause();
    }

    public void Play(AudioClip clip)
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
}
