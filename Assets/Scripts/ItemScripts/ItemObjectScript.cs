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
        
        [Header("1. Is your item an interactable?")]
        public bool interactableWithChoice;
        
        [Header("2. Give it the right int, based on whether it's an interactable or no:")] 
        public int thisObjectInt;
        
        [Header("(3.) If interactable, give it an int to show after being used. Comes after 'Yes' choice:")]
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
            _playerIsInTrigger = true;
            
            ItemController.playerIsInsideItemTrigger = _playerIsInTrigger;

            currentTriggeredObject = gameObject;
            print("CurrentTriggeredObject assigned to: " + currentTriggeredObject.name);
            
            if (autoInteract)
            {
                OpenYourItemScene();
                if (disableAutoInteractAfterFirstAutoInteract)
                {
                    autoInteract = false;
                }
            }
            else
            {
                StartCoroutine(InsideItemTrigger());
            }
            
            if (_cat == currentTriggeredObject)
            {
                StartCoroutine(_catInteractionScript.CheckIfCatShouldStop());
            }
        }

        private IEnumerator InsideItemTrigger()
        {
            yield return new WaitUntil(() => UserInput.Interact || !_playerIsInTrigger);
            if (!_playerIsInTrigger) yield break;
            if (inItemScene)
            {
                print("Player tried to Interact, but was currently in an ItemScene.");
                yield break;
            }
            OpenYourItemScene();
            yield return InsideItemTrigger();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            _playerIsInTrigger = false;
            
            ItemController.playerIsInsideItemTrigger = _playerIsInTrigger;
            if (currentTriggeredObject == gameObject)
            {
                currentTriggeredObject = null;
            }
            
            if (_cat == gameObject)
            {
                StopCoroutine(_catInteractionScript.CheckIfCatShouldStop());
            }
        }
        
        #endregion

        #region --- Opening & Loading Item Scenes ---
        private void OpenYourItemScene()
        {
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
