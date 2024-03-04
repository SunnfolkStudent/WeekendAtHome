using System.Collections;
using Cat;
using PlayerScripts;
using SceneScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace ItemScripts
{
    public class ItemObjectScript : MonoBehaviour
    {
        #region --- Initialization ---
        
        [Header("Int for scrub retrieved in item scenes:")]
        [Space(5f)]
        public int thisObjectInt;
        
        [Header("If your item has a choice / is an Interactable, be sure to check off the below:")]
        public bool interactableWithChoice;
        [Header("A corresponding usedInteractable int, for after 'Yes' choice:")]
        public int usedInteractableInt;
        
        private bool _choiceHasBeenMade;

        [Header("If player enters the object's trigger - autoInteract.")]
        public bool autoInteract;
        public bool disableAutoInteractAfterFirstAutoInteract = true;

        public static int currentObjectInt;
        public static int currentUsedInteractableInt;
        public static bool inItemScene;
        public static bool currentlyOpeningItem;
        public static GameObject currentTriggeredObject;
        private bool _playerIsInTrigger;

        private Scene _itemScene;
        private Scene _interactableScene;

        private GameObject _cat;
        private CatInteractionScript _catInteractionScript;

        private EventSystem _eventSystemMain;
        public static bool enableEventSystemMain;

        private void Start()
        {
            _eventSystemMain = GameObject.FindWithTag("EventSystemMain").GetComponent<EventSystem>();
            if (gameObject != GameObject.FindWithTag("CatPNG")) return;
            _cat = GameObject.FindWithTag("CatPNG");
            _catInteractionScript = _cat.GetComponent<CatInteractionScript>();
        }
        
        #endregion
        
        private void Update()
        {
            if (Keyboard.current.cKey.isPressed)
            {
                print("Loaded scenes currently: " + SceneManager.loadedSceneCount);
                if (currentTriggeredObject == null)
                {
                     print("CurrentTriggeredObject is null.");
                }
                else
                {
                    print("CurrentTriggeredObject: " + currentTriggeredObject.name);
                }
                print("Are we in an ItemScene? " + inItemScene);
            }

            if (SceneManager.loadedSceneCount > 2)
            {
                StartCoroutine(RemoveUnwantedScenes());
            }
            
            if (InteractableItemController.clickedYes && !_choiceHasBeenMade)
            {
                _choiceHasBeenMade = true;
            }

            if (enableEventSystemMain)
            {
                _eventSystemMain.enabled = true;
            }
            else
            {
                _eventSystemMain.enabled = false;
            }
        }
        // <3 --- The below comment is to serve as inspiration for how NOT to code. --- <3
        // if ((UserInput.Interact && _playerIsInTrigger && !InItemCutscene && (!alreadyUsed || !canNotInteractMultiple)) || (autoInteract && _playerIsInTrigger && !InItemCutscene && (!alreadyUsed || !canNotInteractMultiple)))

        #region --- OnTrigger2D ---
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            if (_playerIsInTrigger) return; 
            _playerIsInTrigger = true;
            
            ItemController.playerIsInsideItemTrigger = _playerIsInTrigger;
            
            if (currentTriggeredObject == null)
            {
                currentTriggeredObject = gameObject;
                print("CurrentTriggeredObject assigned to: " + currentTriggeredObject.name);
            }
            else
            {
                Debug.LogWarning("Beep boop, no assigned TriggeredObject to this gameObject: " + gameObject);
            }

            if (currentTriggeredObject != gameObject) return;
            
            if (autoInteract)
            {
                OpenYourItemScene();
                if (disableAutoInteractAfterFirstAutoInteract)
                {
                    autoInteract = false;
                }
            }
            if (_cat == gameObject)
            {
                StartCoroutine(_catInteractionScript.CheckIfCatShouldStop());
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            if (!_playerIsInTrigger) return;
            ItemController.playerIsInsideItemTrigger = _playerIsInTrigger;

            if (UserInput.Interact)
            {
                print("Player tried to Interact");
                if (currentTriggeredObject == gameObject)
                {
                    OpenYourItemScene();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            _playerIsInTrigger = false;
            
            ItemController.playerIsInsideItemTrigger = _playerIsInTrigger;
            currentTriggeredObject = null;
            
            if (_cat == gameObject)
            {
                StopCoroutine(_catInteractionScript.CheckIfCatShouldStop());
            }
        }
        
        #endregion

        #region --- Opening & Loading Item Scenes ---
        private void OpenYourItemScene()
        {
            if (inItemScene) return;
            // This is 2 because of main gameScene + item Scene... (DontDestroyOnLoad Scene doesn't count)
            if (SceneManager.loadedSceneCount > 2)
            {
                StartCoroutine(RemoveUnwantedScenes());
            }
            else
            {
                if (currentlyOpeningItem) return;
                currentlyOpeningItem = true;
                currentObjectInt = thisObjectInt;
                currentUsedInteractableInt = usedInteractableInt;
                
                print("Opening Item Scene of: " + gameObject);
                if (interactableWithChoice)
                {
                    if (_choiceHasBeenMade)
                    {
                        StartCoroutine(LoadAdditiveScene("UsedInteractable")); 
                    }
                    else
                    {
                        StartCoroutine(LoadAdditiveScene("Interactable")); 
                        enableEventSystemMain = false;
                    }
                }
                else
                { StartCoroutine(LoadAdditiveScene("Item")); }
            }
        }
        
        private IEnumerator LoadAdditiveScene(string sceneName)
        {
            if (sceneName != "Interactable" && sceneName != "Item" && sceneName != "UsedInteractable")
            {
                Debug.LogError("Setting up the wrong scene to be active!");
            }
            else
            {
                var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                Debug.Log("Loading the scene with name: " + sceneName);
                yield return new WaitUntil(() => loadSceneAsync.isDone);
            }
        }
        
        #endregion
            
        // This code is a failsafe in the event that bugs happen, to unload all scenes except the main game Scene.
        private IEnumerator RemoveUnwantedScenes()
        {
            var currentAmountOfLoadedScenes = SceneManager.loadedSceneCount;
            
            for (var i = 0; i < currentAmountOfLoadedScenes; ++i)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (scene == LevelLoader.currentGameScene) continue;
                // if (scene == SceneManager.GetActiveScene()) continue;
                
                SceneManager.UnloadSceneAsync(scene);
                currentAmountOfLoadedScenes--;
            }
            if (currentAmountOfLoadedScenes < 2) 
                print("No longer removing scenes because " + currentAmountOfLoadedScenes + " = 1.");
            yield break;
        }
    }
}
