using System.Collections;
using PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ItemScripts
{
    public class ItemObjectScript : MonoBehaviour
    {
        //Variables for what object should load
        [Header("What Scrub It Gets From The Scene")]
        public int thisObjectInt;
    
        [Header("Item Properties:")]
        [Header("If your item doesn't have a choice, be sure to uncheck the below:")]
        public bool interactableWithChoice;
        [Header("If 'Yes' is pressed; cannot be interacted with for the remainder of the scene:")]
        public bool canNotInteractMultiple;
        [Header("If player walks within the object's trigger, auto-interacts with item:")]
        public bool autoInteract;
    
        [Header("Disables Item")]
        public bool alreadyUsed;
    
        // public static Vector2 CurrentObjectSize;
        public static int currentObjectInt;
        public static bool inItemCutscene;
        public static int currentYesAnswer;
    
        private bool _playerIsInTrigger;

        [Header("What Happens On Yes:")]
        [Header("0 = Nothing, 1 = LoadNextScene, 2 = CatBowlFull")]
        [Header("3 = DeathScene FrontDoor")]
            
        private int _whatHappensOnYes;

        private GameObject _mainEventSystem;

        private void Start()
        {
            _mainEventSystem = GameObject.FindWithTag("EventSystemMain");
            inItemCutscene = false;
        }

        //If in trigger load object.
        
        // TODO: Add the autoInteract inside TriggerEnter2D vs having it in Update.
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerIsInTrigger = true;
                if (autoInteract)
                {
                    StartCoroutine(OpenOrCloseItemScene());
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
            if (UserInput.Interact && _playerIsInTrigger)
            {
                StartCoroutine(OpenOrCloseItemScene());
            }
            // The below comment is to serve as inspiration for how NOT to code. <3
            // if ((UserInput.Interact && _playerIsInTrigger && !InItemCutscene && (!alreadyUsed || !canNotInteractMultiple)) || (autoInteract && _playerIsInTrigger && !InItemCutscene && (!alreadyUsed || !canNotInteractMultiple)))
        }

        // TODO: Figure out how to solve 3 instances being sent in when pressing the button once.
        
        private IEnumerator OpenOrCloseItemScene()
        {
            if (alreadyUsed) yield break;
            if (canNotInteractMultiple) yield break;

            if (!inItemCutscene)
            {
               OpenItemScene();
            }
            else if (inItemCutscene)
            {
                CloseItemScene();
            }
            yield return new WaitForSeconds(1f);
        }

        private void OpenItemScene()
        {
            currentYesAnswer = _whatHappensOnYes;
            currentObjectInt = thisObjectInt;

            // _mainEventSystem.SetActive(false);
            Time.timeScale = 0;

            print("Opening Item Scene");
            if (interactableWithChoice)
            {
                SceneManager.LoadScene("InteractableItem", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.LoadScene("Item", LoadSceneMode.Additive);
            }
            inItemCutscene = true; 
        }

        private void CloseItemScene()
        {
            print("Closing Item Scene");
            // _mainEventSystem.SetActive(true);
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("Item");
            inItemCutscene = false;
        }
    }
}
