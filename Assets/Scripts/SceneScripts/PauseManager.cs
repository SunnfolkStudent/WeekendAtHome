using System;
using System.Collections;
using NUnit.Framework;
using PlayerScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SceneScripts
{
    public class PauseManager : MonoBehaviour
    {
        // Declare Variables 
        /*private PlayerControls controls;  */
        
        [SerializeField] private GameObject pauseScene;
        public UserInput userInput;
    
        // Initialize the controls
        /*private void Awake() => controls = new PlayerControls();
        private void OnEnable() => controls.Enable();
        private void OnDisable() => controls.Disable();*/

        private void Start()
        {
            pauseScene = GameObject.FindGameObjectWithTag("PauseScene");
            if (pauseScene)
            {
                SetPauseScreenInactive();
            }
        }

        private void Update()
        {
            if (DataTransfer.IsPause) return;

            // Check if escape is pressed
            if (UserInput.Pause && !DataTransfer.IsPause)
            {
                SetPauseScreenActive();
            }
            else if (!UserInput.Unpause && DataTransfer.IsPause)
            {
                SetPauseScreenInactive();
            }

            /*if (!DataTransfer.IsPause)
            {
                StartCoroutine(SetPauseScreenActive());
            }
            else if (DataTransfer.IsPause)
            { 
                StartCoroutine(SetPauseScreenInactive());
            }*/
        }

        public void SetPauseScreenActive()
        {
            Time.timeScale = 0f;
            userInput.SwitchInputToPauseScreen();
            pauseScene.SetActive(true);
            DataTransfer.IsPause = true;
        }

        public void SetPauseScreenInactive()
        {
            Time.timeScale = 1f;
            userInput.SwitchInputToGameScene();
            pauseScene.SetActive(false);
            DataTransfer.IsPause = false; 
        }
        
    }
}
