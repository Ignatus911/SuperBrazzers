
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public List<Sound> sounds;

    private void Awake()
    {
        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
        }
    }

    public void Play(string soundName)
    {
       foreach(Sound sound in sounds)
        {
            if (sound.clip.name == soundName)
            {
                sound.source.Play();
                return;
            }
            else Debug.Log(soundName + " has not been found!");
        }
    }
}
