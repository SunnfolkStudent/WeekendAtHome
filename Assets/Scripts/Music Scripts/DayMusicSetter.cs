using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayMusicSetter : MonoBehaviour
{
    private String currentlyPlaying;

    public String musicToPlay;
    
    // Start is called before the first frame update
    void Start()
    {
        currentlyPlaying = FindAnyObjectByType<MusicManager>().musicSource.ToString();
        FindAnyObjectByType<MusicManager>().StopPlay(currentlyPlaying);
        
        if (!musicToPlay.Equals("Nothing"))
            FindAnyObjectByType<MusicManager>().Play(musicToPlay);
    }

}
