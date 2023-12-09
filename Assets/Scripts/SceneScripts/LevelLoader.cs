using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;

    public bool noFadeOut;

    public float transitionTime = 1;

    public void Start()
    {
        if (noFadeOut)
            transition.Play("Crossfade_Idle");
    }

    public void loadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        return;
    }

    public void loadSceneByName(string sceneName)
    {
        StartCoroutine(LoadLevel(SceneManager.GetSceneByName(sceneName).buildIndex + 1));
        return;
    }
    
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("StartFade");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
