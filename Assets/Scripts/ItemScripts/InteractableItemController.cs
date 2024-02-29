using SceneScripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace ItemScripts
{
    public class InteractableItemController : MonoBehaviour
    {
        // TODO: Fix the interactable items, cuz they work, but there are no timelines going on. : )

        private LevelLoader _levelLoader;
        public ItemType[] itemScrub;
        public TMP_Text itemName;
        public TMP_Text itemText;
        public Image itemImage;
        public AudioSource audioPlayer;
        public PlayableAsset itemTimeline;
        public PlayableDirector playableDirector;
        
        // TODO: Check if the new Timeline code works : )

        private string _sceneToLoad;

        private void Awake()
        {
            playableDirector = GetComponent<PlayableDirector>();
            playableDirector.paused += DirectorPaused;
            playableDirector.played += DirectorPlaying;
            playableDirector.stopped += DirectorStopped;
        }
        
        private void Start()
        {
            _levelLoader = GetComponent<LevelLoader>();
            audioPlayer = GetComponent<AudioSource>();
            audioPlayer.clip = itemScrub[ItemObjectScript.currentObjectInt].itemAudio;
            //audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.currentObjectInt].itemAudio);

            itemName.text = itemScrub[ItemObjectScript.currentObjectInt].itemName;
            itemText.text = itemScrub[ItemObjectScript.currentObjectInt].itemText;
            itemImage.sprite = itemScrub[ItemObjectScript.currentObjectInt].itemImage;
            itemImage.transform.localScale = itemScrub[ItemObjectScript.currentObjectInt].itemSize;
            
            itemTimeline = itemScrub[ItemObjectScript.currentObjectInt].timeline;
            itemTimeline = playableDirector.playableAsset;
        }
        
        public void OnClickYes()
        {
            Time.timeScale = 1;
            audioPlayer.clip = itemScrub[ItemObjectScript.currentObjectInt].cutSceneAudio;
            audioPlayer.Play();
            audioPlayer.loop = true;

            if (itemTimeline != null)
            {
                playableDirector.Play(itemTimeline);
            }
            
            // audioPlayer.Play(itemScrub[ItemObjectScript.currentObjectInt].cutSceneAudio);
            /*switch (ItemObjectScript.currentYesAnswer)
            {
                case 1: // Player goes to bed/couch, plays cutscene and then loads the next scene
                    if (itemName.text == "Bed")
                    {
                        LoadNextLevelFromBed();
                    }
                    else
                    {
                        LoadNextLevelFromCouch();
                    }
                    // Invoke(nameof(_levelLoader.LoadNextLevelByIndex), _levelLoader.transitionTime + 0.1f);
                    break;
                case 2: // Item timeline is being played.
                    _playableDirector.Play(itemTimeline);
                    break;
                case 3: // Load DeathScene by FrontDoor
                    Invoke(nameof(PlayDeathCredits),22f);
                    break;
            }*/
        }
    
        public void OnClickNo()
        {
            Time.timeScale = 1;
            Debug.Log("Leaving Interactable Scene");
            SceneManager.UnloadSceneAsync("InteractableItem");
            ItemObjectScript.inItemCutscene = false;
        }

        private void DirectorPlaying(PlayableDirector obj)
        {
            // TODO: Add code here for disabling PlayerInput (OnDisable?) and other effects to let timeline play as it should.
            // Maybe a reference to the GameController for a method in there would be good.
        }
        
        private void DirectorPaused(PlayableDirector obj)
        {
            // TODO: Add code here for pauseMenu stuff to be enabled.
            // Maybe a reference to the GameController for a method in there would be good.
        }

        private void DirectorStopped(PlayableDirector obj)
        {
            // TODO: Add code here for pauseMenu stuff to be enabled.
            // Maybe a reference to the GameController for a method in there would be good.
        }
    }
}
