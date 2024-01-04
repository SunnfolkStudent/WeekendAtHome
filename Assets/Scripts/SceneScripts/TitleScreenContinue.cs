using System;
using System.Collections;
using System.Collections.Generic;
using SceneScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScreenContinue : MonoBehaviour
{
    // Declare variables
    private PlayerControls controls;
    [SerializeField] private LevelLoader levelLoader;
    
    // Initialize player controls
    private void Awake() => controls = new PlayerControls();
    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private String currentlyPlaying;
    private void Start()
    {
        currentlyPlaying = FindAnyObjectByType<MusicManager>().musicSource.ToString();
    }

    void Update()
    {
        // Check if any key is pressed and start to load the next level
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            Application.Quit();
        
        if (controls.AnyButton.AnyKey.triggered)
            levelLoader.LoadNextLevelByIndex();
    }
}
