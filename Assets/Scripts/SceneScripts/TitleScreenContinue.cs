using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SceneScripts
{
    public class TitleScreenContinue : MonoBehaviour
    {
        // Declare variables
        private PlayerControls _controls;
        [SerializeField] private LevelLoader levelLoader;
    
        // Initialize player controls
        private void Awake() => _controls = new PlayerControls();
        private void OnEnable() => _controls.Enable();
        private void OnDisable() => _controls.Disable();

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
        
            if (_controls.TitleScreen.AnyKeyorStart.triggered)
                levelLoader.LoadNextLevelByIndex();
        }
    }
}
