using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCreditsLevelLoaderTimer : MonoBehaviour
{
    
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
        if (timerDone)
            levelLoader.loadSceneByName("TitleScene");
    }

    IEnumerator timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(25);
            timerDone = true;
        }
    }
}
