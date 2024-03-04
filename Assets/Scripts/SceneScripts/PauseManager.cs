using System.Collections;
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
            if (UserInput.Escape)
            {
                StartCoroutine(AreWePausing(false));
            }
            if (UserInput.PauseDuringCutscene)
            {
                StartCoroutine(AreWePausing(true));
            }
            if (UserInput.Unpause)
            {
                SetPauseScreenInactive();
            }
        }

        private IEnumerator AreWePausing(bool duringCutscene)
        {
            if (ItemObjectScript.inItemScene) yield break;
            pausedDuringCutscene = duringCutscene;
            print("Setting pauseScreen Active.");
            SetPauseScreenActive();
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
