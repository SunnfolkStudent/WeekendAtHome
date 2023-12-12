using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenContinue : MonoBehaviour
{
    // Declare variables
    private PlayerControls controls;
    [SerializeField] private LevelLoader levelLoader;
    
    // Initialize player controls
    private void Awake() => controls = new PlayerControls();
    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();
    
    void Update()
    {
        // Check if any key is pressed and start to load the next level
        
        if (controls.AnyButton.AnyKey.triggered)
            levelLoader.loadNextLevel();
    }
}
