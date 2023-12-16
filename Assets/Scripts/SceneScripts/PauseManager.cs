using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneScripts
{
    public class PauseManager : MonoBehaviour
    {

        // Declare Variables 
        private PlayerControls controls;

        public static bool pauseSceneIsActive = false;
    
        // Initialize the controls
        private void Awake() => controls = new PlayerControls();
        private void OnEnable() => controls.Enable();
        private void OnDisable() => controls.Disable();

        public void UnloadPauseScene()
        {
            // Unload the pause scene
            SceneManager.UnloadSceneAsync("PauseOverlayScene");
            return;
        }

        public void SetPauseSceneIsActiveToInactive()
        {
            // Resume the time
            Time.timeScale = 1f;
            pauseSceneIsActive = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioListener>().enabled = true;
            GameObject.FindGameObjectWithTag("EventSystemMain").SetActive(true);
            return;
        }
    
        void Update()
        {
            // Check if escape is pressed
            if (controls.Pause.OpenMenu.triggered)
            {
                if (!pauseSceneIsActive)
                {
                    // Stops time and adds the pause scene
                    Time.timeScale = 0f;
                    SceneManager.LoadScene("PauseOverlayScene", LoadSceneMode.Additive);
                    pauseSceneIsActive = true;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<AudioListener>().enabled = false;
                    GameObject.FindGameObjectWithTag("EventSystemMain").SetActive(false);
                }
            }
        }
    }
}
