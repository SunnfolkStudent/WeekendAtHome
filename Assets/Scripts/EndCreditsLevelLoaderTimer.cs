using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCreditsLevelLoaderTimer : MonoBehaviour
{
    //Declare variables
    private LevelLoader levelLoader;

    private bool timerDone = false;

    void Start()
    {
        levelLoader = GetComponent<LevelLoader>();
        StartCoroutine(timer());
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the timer is done before loading the title scene
        if (timerDone)
            levelLoader.loadSceneByName("TitleScene");
    }

    IEnumerator timer()
    {
        // set timerDone to true after 23 seconds
        while (true)
        {
            yield return new WaitForSeconds(35.7f);
            timerDone = true;
        }
    }
}
