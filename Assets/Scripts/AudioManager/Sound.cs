using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public AudioClip clip;
    [SerializeField, Range(0f, 1f)] public float volume;

    [HideInInspector] public AudioSource source;

}
