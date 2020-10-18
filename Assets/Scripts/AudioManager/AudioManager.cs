using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] public List<Sound> sounds;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
        }
    }

    private void CreateClip(AudioClip clip, float volume = 1)
    {
        var sound = new Sound();
        sound.clip = clip;
        sound.volume = volume;
        sound.source = gameObject.AddComponent<AudioSource>();
        sound.source.clip = sound.clip;
        sound.source.volume = sound.volume;
        sounds.Add(sound);
        sound.source.Play();
    }

    public void Play(AudioClip clip)
    {
        foreach (var sound in sounds)
        {
            if (sound.clip == clip)
            {
                sound.source.Play();
                return;
            }
        }
        CreateClip(clip, 1);
    }

    public void Stop(AudioClip clip)
    {
        foreach (var sound in sounds)
        {
            if (sound.clip == clip)
            {
                sound.source.Stop();
                return;
            }
        }
    }
}
