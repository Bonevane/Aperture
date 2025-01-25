using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float MasterVolume = 1.0f;
    public Sound[] sounds;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.volume = s.volume * MasterVolume;
        s.source.Play();
    }

    public void Play(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.volume = volume * MasterVolume;
        s.source.Play();
    }

    private void Update()
    {
        if(CompareTag("MusicPlayer"))
            MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.7f);
        else
            MasterVolume = PlayerPrefs.GetFloat("MasterVolumeSFX", 1f);
    }

    public bool IsPlaying(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return false;
        }
        else
            return s.source.isPlaying;
    }

    public void UpdateCurrentPlayingVolume()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * MasterVolume;
        }
    }
}
