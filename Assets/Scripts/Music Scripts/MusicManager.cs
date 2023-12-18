using System;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Serialization;

public class MusicManager : MonoBehaviour
{
    public Music[] tracks;

    public static MusicManager instance;
    public Music musicSource;
    
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

        foreach (Music m in tracks)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            musicSource = m;
            m.source.clip = m.clip;

            m.source.volume = m.Volume;
            m.source.loop = m.loop;
        }
    }

    private void Start()
    {
        //Play("");
    }

    public void Play(string name)
    {
        //source.source.Stop();
        Music m = Array.Find(tracks, track => track.name == name);
        musicSource = m;
        if (m == null)
        {
            Debug.Log("Track: " + name + " not found!");
            return;
        }
        else
        {
            m.source.Play();
        }
    }

    public void StopPlay(string name)
    {
        if (musicSource == null)
        {
            Debug.Log("Track: " + name + " not found!");
            return;
        }
        else
        {
            musicSource.source.Stop();
        }

    }
}