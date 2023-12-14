using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts 
{ 
    public class PauseMenu : MonoBehaviour
    {
        /* [SerializeField] private PlayerInput input;

        [Header("PauseMenu")]
        [SerializeField] private bool isPaused;
        [SerializeField] private GameObject paused;
        [SerializeField] private GameObject resume;
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject controls;

        private void Start()
        {
            Unpause();
        }

        private void Update()
        {
            if (!input.pause) return;

            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }

        private void Pause()
        {
            isPaused = true;
            Cursor.visible = true;

            paused.SetActive(true);
            resume.SetActive(true);
            mainMenu.SetActive(true);
            controls.SetActive(true);

            Time.timeScale = 0;
        }

        public void Unpause()
        {
            isPaused = false;
            Cursor.visible = false;

            paused.SetActive(false);
            resume.SetActive(false);
            mainMenu.SetActive(false);
            controls.SetActive(false);
            
            Time.timeScale = 1;
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        } */
    } 
}