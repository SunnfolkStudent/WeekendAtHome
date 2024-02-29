using ItemScripts;
using PlayerScripts;
using UnityEngine;

namespace SceneScripts
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseScene;
        public UserInput userInput;

        [SerializeField] private bool pausedDuringCutscene;

        private void Awake()
        {
            pauseScene = GameObject.FindGameObjectWithTag("PauseScene");
        }

        private void Start()
        {
            if (pauseScene.activeSelf)
            {
                DataTransfer.isPause = false;
                SetPauseScreenInactive();
            }
        }

        private void Update()
        {
            if (UserInput.Pause)
            {
                if (ItemObjectScript.inItemCutscene) return; 
                SetPauseScreenActive();
                pausedDuringCutscene = false;
            }
            else if (UserInput.PauseDuringCutscene)
            {
                if (ItemObjectScript.inItemCutscene) return; 
                SetPauseScreenActive();
                pausedDuringCutscene = true;
            }
            else if (UserInput.Unpause)
            {
                SetPauseScreenInactive();
            }
        }

        private void SetPauseScreenActive()
        {
            Time.timeScale = 0f;
            userInput.SwitchInputToPauseScreen();
            pauseScene.SetActive(true);
            DataTransfer.isPause = true;
        }

        public void SetPauseScreenInactive()
        {
            Time.timeScale = 1f;
            if (pausedDuringCutscene)
            {
                userInput.SwitchInputToCutscene();
            }
            else
            {
                userInput.SwitchInputToGameScene();
            }
            pausedDuringCutscene = false;
            pauseScene.SetActive(false);
            DataTransfer.isPause = false; 
        }
    }
}
