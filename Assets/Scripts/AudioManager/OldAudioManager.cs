//using System.Collections.Generic;
//using UnityEngine;

//public class OldAudioManager : MonoBehaviour
//{
//    public static OldAudioManager Instance { get; private set; }

//    [SerializeField] private AudioClip mainTheme;
//    [SerializeField] private AudioSource mainThemeSoure;
//    [SerializeField] private AudioClip pauseSound;
//    [SerializeField] private bool onPause;
//    [SerializeField] public List<Sound> sounds;



//    private void Awake()
//    {
//        if (Instance == null)
//            Instance = this;
//        else
//        {
//            Destroy(gameObject);
//            return;
//        }

//        foreach (var sound in sounds)
//        {
//            sound.source = gameObject.AddComponent<AudioSource>();
//            sound.source.clip = sound.clip;
//            sound.source.volume = sound.volume;
//        }

//        TimeController.OnPauseGame += OnPauseGame;

//        mainThemeSoure.clip = mainTheme;
//        mainThemeSoure.Play();
//    }
//    private void OnPauseGame(bool timeIsStoped)
//    {
//        onPause = timeIsStoped;
//        PressedPauseInGame();
//    }

//    private void CreateClip(AudioClip clip, float volume = 1)
//    {
//        var sound = new Sound();
//        sound.clip = clip;
//        sound.volume = volume;
//        sound.source = gameObject.AddComponent<AudioSource>();
//        sound.source.clip = sound.clip;
//        sound.source.volume = sound.volume;
//        sounds.Add(sound);
//        sound.source.Play();
//    }

//    public void Play(AudioClip clip)
//    {
//        foreach (var sound in sounds)
//        {
//            if (sound.clip == clip)
//            {
//                if (sound.source.isPlaying)
//                {
//                    sound.source.Stop();
//                }
//                sound.source.Play();
//                return;
//            }
//        }
//        CreateClip(clip, 1);
//    }

//    public void Stop(AudioClip clip)
//    {
//        foreach (var sound in sounds)
//        {
//            if (sound.clip == clip)
//            {
//                sound.source.Stop();
//                return;
//            }
//        }
//    }

//    private void PressedPauseInGame()
//    {

//        if (onPause)
//        {
//            mainThemeSoure.Pause();
//            foreach (var sound in sounds)
//            {
//                if (sound.source.isPlaying)
//                {
//                    sound.source.Stop();
//                }
//            }
//            Debug.Log("Set on pause");
//            return;
//        }
//        else if (!onPause)
//        {
//            Debug.Log("Unpause");
//            mainThemeSoure.UnPause();
//            foreach (var sound in sounds)
//            {
//                if (sound.soundOnPause)
//                {
//                    sound.source.UnPause();
//                    sound.soundOnPause = false;
//                }
//            }
//        }
//        Play(pauseSound);
//    }
//}
