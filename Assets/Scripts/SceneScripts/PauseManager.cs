using System;
using PlayerScripts;
using Unity.VisualScripting;
using UnityEngine;

namespace SceneScripts
{
    public class PauseManager : MonoBehaviour
    {
        // Declare Variables 
        /*private PlayerControls controls;  */

        [SerializeField] private bool pauseSceneIsActive;
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
            pauseSceneIsActive = false;
        }

        private void Start()
        {
            throw new NotImplementedException();
        }

        void Update()
        {
            // Check if escape is pressed
            if (!UserInput.Pause) return;
            
            if (!pauseSceneIsActive)
            {
                SetPauseScreenActive();
            }
            else if (pauseSceneIsActive)
            { 
                SetPauseScreenInactive();
            }
        }

        public void SetPauseScreenActive()
        {
            Time.timeScale = 0f;
            pauseScene.SetActive(true);
            eventSystemMain.SetActive(false);
            pauseSceneIsActive = true;
        }

        public void SetPauseScreenInactive()
        {
            Time.timeScale = 1f;
            pauseScene.SetActive(false);
            eventSystemMain.SetActive(true);
            pauseSceneIsActive = false; 
        }
        
    }
}
