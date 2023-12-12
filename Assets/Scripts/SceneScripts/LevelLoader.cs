using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    
    //Declare variables
    public Animator transition;
    public bool noFadeOut;
    public float transitionTime = 1;

    public void Start()
    {
        //Play a no-fade animation if there is no fade out requested
        if (noFadeOut)
            transition.Play("Crossfade_Idle");
    }

    public void loadNextLevel()
    {
        //Start a coroutine to wait until the fade is done
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        return;
    }

    public void loadSceneByName(string sceneName)
    {
        //Start a coroutine to wait until the fade is done
        StartCoroutine(LoadLevel(SceneManager.GetSceneByName(sceneName).buildIndex + 1));
        return;
    }
    
    IEnumerator LoadLevel(int levelIndex)
    {
        //Start the fade animation and then wait until the tansition time elapses
        transition.SetTrigger("StartFade");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
