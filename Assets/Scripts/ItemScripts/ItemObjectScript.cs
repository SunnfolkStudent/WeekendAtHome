using PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace ItemScripts
{
    public class ItemObjectScript : MonoBehaviour
    {
        //Variables for what object should load
        [Header("What Scrub It Gets From The Scene")]
        public int thisObjectInt;
    
        [Header("Item Properties")]
        public bool interactableWithChoice;
        public bool canNotInteractMultiple;
        public bool autoInteract;
    
        [Header("Disables Item")]
        public bool alreadyUsed;
    
        public static Vector2 CurrentObjectSize;
        public static int CurrentObjectInt;
        public static bool InItemCutscene;
        public static int CurrentYesAnswer;
    
        private bool _playerIsInTrigger;

        [Header("What Happens On Yes:")]
        [Header("0 = Nothing, 1 = LoadNextScene, 2 = CatBowlFull")]
        [Header("3 = DeathScene FrontDoor")]
            
        public int whatHappensOnYes;

        private GameObject _mainEventSystem;

        private void Start()
        {
            _mainEventSystem = GameObject.FindWithTag("EventSystemMain");
            InItemCutscene = false;
        }

        //If in trigger load object.
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _playerIsInTrigger = true;
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
            if ((UserInput.Interact && _playerIsInTrigger && !InItemCutscene && (!alreadyUsed || !canNotInteractMultiple)) || (autoInteract && _playerIsInTrigger && !InItemCutscene && (!alreadyUsed || !canNotInteractMultiple)))
            {
                _mainEventSystem.SetActive(false);
                Time.timeScale = 0;
                if (autoInteract && !canNotInteractMultiple)
                {
                    _playerIsInTrigger = false;
                }

                CurrentYesAnswer = whatHappensOnYes;
                CurrentObjectInt = thisObjectInt;
                alreadyUsed = true;
                if (interactableWithChoice)
                {
                    InItemCutscene = true;
                    SceneManager.LoadScene("InteractableItem",LoadSceneMode.Additive);
                }
                else
                {
                    InItemCutscene = true;
                    SceneManager.LoadScene("Item",LoadSceneMode.Additive);
                }
            }
        }
    }
}
