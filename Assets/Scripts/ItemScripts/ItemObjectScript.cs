using System.Collections;
using PlayerScripts;
using SceneScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemScripts
{
    public class ItemObjectScript : MonoBehaviour
    {
        // TODO: For fun; Make an Editor script that shows scrub in Inspector, using "thisObjectInt" & "interactableWithChoice".
        
        //Variables for what object should load
        [Header("What Scrub It Gets From The Scene:")]
        [Header("Remember to adjust the below int, depending on if it's interactable or not:")]
        [Space(5f)] 
        public int thisObjectInt;
    
        [Header("Item Properties:")]
        [Header("If your item has a choice, be sure to check off the below:")]
        public bool interactableWithChoice;
        
        [Header("If 'Yes' is pressed; cannot be interacted with for the remainder of the scene:")]
        public bool canNotInteractMultiple;
        
        [Header("If player enters the object's trigger, auto-interacts with item:")]
        public bool autoInteract;
        
        public static int currentObjectInt;
        public static bool inItemCutscene;
    
        private bool _playerIsInTrigger;

        private void Start()
        {
            inItemCutscene = false;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerIsInTrigger = true;
                if (autoInteract)
                {
                    OpenItemScene();
                    autoInteract = false;
                }
            }
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerIsInTrigger = false;
            }
        }

        //If Interact is pressed when near, open the correct scene
        
        private void Update()
        {
            if (DataTransfer.isPause) return; 
            if (UserInput.Interact)
            {
                if (!_playerIsInTrigger) return;
                if (canNotInteractMultiple) return;
                
                if (inItemCutscene)
                {
                    CloseItemScene();
                }
                else
                {
                    OpenItemScene();
                }
            }
            else if (UserInput.Pause)
            {
                if (!_playerIsInTrigger) return;
                
                CloseItemScene();
            }
        }
        
                // <3 --- The below comment is to serve as inspiration for how NOT to code. --- <3
        // if ((UserInput.Interact && _playerIsInTrigger && !InItemCutscene && (!alreadyUsed || !canNotInteractMultiple)) || (autoInteract && _playerIsInTrigger && !InItemCutscene && (!alreadyUsed || !canNotInteractMultiple)))
        
        private void OpenItemScene()
        {
            currentObjectInt = thisObjectInt;

            print("Opening Item Scene");
            if (interactableWithChoice)
            {
                if (canNotInteractMultiple)
                {
                    InteractableAlreadyUsed();
                    inItemCutscene = true;
                    return;
                }
                SceneManager.LoadSceneAsync("InteractableItem", LoadSceneMode.Additive);
                StartCoroutine(TurnAdditiveSceneIntoActiveScene(8));
                // TODO: Set up to automatically find the index of the InteractableItemScene.
            }
            else
            {
                SceneManager.LoadSceneAsync("Item", LoadSceneMode.Additive);
                StartCoroutine(TurnAdditiveSceneIntoActiveScene(7));
                // TODO: Set up to automatically find the index of the ItemScene.
            }
            inItemCutscene = true; 
        }

        private void InteractableAlreadyUsed()
        {
            SceneManager.LoadSceneAsync("Item", LoadSceneMode.Additive);
            StartCoroutine(TurnAdditiveSceneIntoActiveScene(7));
            // TODO: Set up to automatically find the index of the ItemScene.
            // TODO: Create usedInteractable item scenes.
        }

        private IEnumerator TurnAdditiveSceneIntoActiveScene(int sceneIndex)
        {
            yield return new WaitForSeconds(0.06f);
            // We have to wait at least a frame after a scene is loaded, before we can turn it active. This delay ensures we wait till its ready.
            if (SceneManager.GetActiveScene().buildIndex == LevelLoader.currentGameSceneIndex)
            {
                yield return new WaitForSeconds(0.06f);
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
        }

        private void CloseItemScene()
        {
            var currentlyActiveScene = SceneManager.GetActiveScene().name;

            SceneManager.UnloadSceneAsync(currentlyActiveScene);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(LevelLoader.currentGameSceneIndex));
            inItemCutscene = false;
        }
    }
}
