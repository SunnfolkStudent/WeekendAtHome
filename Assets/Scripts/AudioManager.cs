using System;
using Unity.VisualScripting;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    public Sound soundSource;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            soundSource = s;
            s.source.clip = s.clip;

            s.source.volume = s.Volume;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        //surce.source.Stop();
        Sound s = Array.Find(sounds, sound => sound.name == name);
        soundSource = s;
        if (s == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
        else
        {
            s.source.Play();
        }
    }
    public void StopPlay(string name)
    {
        if (soundSource == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
        else
        {
            soundSource.source.Stop();
        }

    }
}



//FindAnyObjectByType<AudioManager>().Play("");