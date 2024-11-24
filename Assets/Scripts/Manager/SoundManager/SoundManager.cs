using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public Sound[] sounds;
    private static Dictionary<string, float> soundTimerDictionary;
    public static List<string> activeSoundList = new List<string>();

    public float musicVolume = .5f; // Müzik ses seviyesi
    public float soundVolume = .5f; // Efekt ses seviyesi

    public static SoundManager instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        soundTimerDictionary = new Dictionary<string, float>();

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.isLoop;

            if (sound.hasCooldown)
            {
                soundTimerDictionary[sound.name] = 0f;
            }
        }
    }

    private void Start()
    {
        GameManager.Instance.OnGameDataLoaded += OnDataLoad;
        
    }
    
    void OnDataLoad()
    {
        Play("Theme");
    }

    public void Play(string name, float startTime = 0f)
    {
        // Ses dosyasýný bul
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        if (!CanPlaySound(sound)) return;

        // Ses seviyesini ayarla
        sound.source.volume = sound.isMusic ? musicVolume/100 : soundVolume;

        // Çalma noktasýný belirle
        if (startTime > 0f)
        {
            if (startTime >= sound.source.clip.length)
            {
                Debug.LogError("Start time exceeds clip length for sound " + name);
                return;
            }

            sound.source.time = startTime;
        }

        // Sesi oynat
        sound.source.Play();
    }


    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogError("Sound " + name + " Not Found!");
            return;
        }

        sound.source.Stop();
    }

    private static bool CanPlaySound(Sound sound)
    {
        if (soundTimerDictionary.ContainsKey(sound.name))
        {
            float lastTimePlayed = soundTimerDictionary[sound.name];

            if (lastTimePlayed + sound.clip.length < Time.time)
            {
                soundTimerDictionary[sound.name] = Time.time;
                return true;
            }

            return false;
        }

        return true;
    }

    // Ses seviyelerini ayarlamak için metodlar
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        foreach (Sound sound in sounds)
        {
            if (sound.isMusic) sound.source.volume = musicVolume / 10;
        }
    }

    public void SetSoundVolume(float volume)
    {
        soundVolume = volume;
        foreach (Sound sound in sounds)
        {
            if (!sound.isMusic) sound.source.volume = soundVolume;
        }
    }
}
