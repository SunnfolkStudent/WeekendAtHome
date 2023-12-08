using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    private PlayerControls controls;

    public static bool pauseSceneIsActive = false;
    
    private void Awake() => controls = new PlayerControls();
    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    public void UnloadPauseScene()
    {
        SceneManager.UnloadSceneAsync("PauseOverlayScene");
        return;
    }

    public void SetPauseSceneIsActiveToInactive()
    {
        Time.timeScale = 1f;
        pauseSceneIsActive = false;
        return;
    }
    
    void Update()
    {
        if (controls.Pause.OpenMenu.triggered)
        {
            if (!pauseSceneIsActive)
            {
                Time.timeScale = 0f;
                SceneManager.LoadScene("PauseOverlayScene", LoadSceneMode.Additive);
                pauseSceneIsActive = true;
            }
        }
    }
}
