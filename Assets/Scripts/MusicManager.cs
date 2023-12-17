using System;
using UnityEngine.Audio;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Music[] sounds;

    public static MusicManager instance;
    public Music surce;
    
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

        foreach (Music s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            surce = s;
            s.source.clip = s.clip;

            s.source.volume = s.Volume;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        //Play("");
    }

    public void Play(string name)
    {
        //surce.source.Stop();
        Music s = Array.Find(sounds, sound => sound.name == name);
        surce = s;
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
        if (surce == null)
        {
            Debug.Log("Sound: " + name + " not found!");
            return;
        }
        else
        {
            surce.source.Stop();
        }

    }
}