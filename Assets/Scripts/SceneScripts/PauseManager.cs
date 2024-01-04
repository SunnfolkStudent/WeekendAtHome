using System;
using System.Collections;
using NUnit.Framework;
using PlayerScripts;
using Unity.VisualScripting;
using UnityEngine;

namespace SceneScripts
{
    public class PauseManager : MonoBehaviour
    {
        // Declare Variables 
        /*private PlayerControls controls;  */
        
        [SerializeField] private GameObject pauseScene;
        [SerializeField] private GameObject eventSystemMain;
    
        // Initialize the controls
        /*private void Awake() => controls = new PlayerControls();
        private void OnEnable() => controls.Enable();
        private void OnDisable() => controls.Disable();*/

        private void Awake()
        {
            pauseScene = GameObject.FindGameObjectWithTag("PauseScene");
            eventSystemMain = GameObject.FindWithTag("EventSystemMain");
            StartCoroutine(SetPauseScreenInactive());
        }

        void Update()
        {
            // Check if escape is pressed
            if (!UserInput.Pause) return;
            
            if (!DataTransfer.IsPause)
            {
                StartCoroutine(SetPauseScreenActive());
            }
            else if (DataTransfer.IsPause)
            { 
                StartCoroutine(SetPauseScreenInactive());
            }
        }

        private IEnumerator SetPauseScreenActive()
        {
            Time.timeScale = 0f;
            pauseScene.SetActive(true);
            eventSystemMain.SetActive(false);
            DataTransfer.IsPause = true;
            yield break;
        }

        public IEnumerator SetPauseScreenInactive()
        {
            Time.timeScale = 1f;
            pauseScene.SetActive(false);
            eventSystemMain.SetActive(true);
            DataTransfer.IsPause = false; 
            yield break;
        }
        
    }
}
