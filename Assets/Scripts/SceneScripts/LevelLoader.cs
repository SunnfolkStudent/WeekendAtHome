using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneScripts
{
    public class LevelLoader : MonoBehaviour
    {
        //Declare variables
        public GameObject transferBetweenLoad;
        public Animator transition;
        public bool noFadeOut;
        public float transitionTime = 2f;
        
        [Header("Transition Speed plays at default speed at 1")]
        [SerializeField] private float transitionSpeed = 0.5f;
        private static readonly int StartFade = Animator.StringToHash("StartFade");

        public void Start()
        {
            transition.speed = transitionSpeed;
            //Play a no-fade animation if there is no fade out requested
            if (noFadeOut)
                transition.Play("Crossfade_Idle");
            else
            {
                transition.Play("Crossfade_End");
            }
        }

        public void LoadNextLevelByIndex()
        {
            //Start a coroutine to wait until the fade is done
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
            return;
        }

        public void LoadSceneByName(string sceneName)
        {
            //Start a coroutine to wait until the fade is done
            StartCoroutine(LoadLevel(SceneManager.GetSceneByName(sceneName).buildIndex+1));
            return;
        }

        public void LoadTitleScreen(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            Destroy(transferBetweenLoad);
        }

        private IEnumerator LoadLevel(int levelIndex)
        {
            //Start the fade animation and then wait until the transition time elapses
            transition.SetTrigger(StartFade);
            yield return new WaitForSeconds(transitionTime);
            SceneManager.LoadScene(levelIndex);
        }
    }
}
