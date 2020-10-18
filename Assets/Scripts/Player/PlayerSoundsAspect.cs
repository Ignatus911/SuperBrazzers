using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsAspect : MonoBehaviour
{

    [SerializeField] List<AudioClip> audioList;
    private AudioSource source;

    // Start is called before the first frame update
    private void Awake()
    {
        source = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        PlaySound("Pipe");
    }

    private void PlaySound( string soundsName)
    {
        foreach(AudioClip audio in audioList)
        {
            if (audio.name == soundsName)
                source.PlayOneShot(audio);
        }
    }
}
