using System;
using Cat;
using SceneScripts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ItemScripts
{
    public class InteractableItemController : MonoBehaviour
    {
        public ItemType[] itemScrub;
        public TMP_Text itemName;
        public TMP_Text itemText;
        public Image itemImage;
        public AudioSource audioPlayer;
        public PlayableDirector timeline;
        public PlayableDirector crossFade;
        public PlayableDirector evening2;
        public PlayableDirector evening3;
        private GameObject _levelLoaderObject;
        public LevelLoader levelLoader;

        private GameObject _mainEventSystem;

        private string _sceneToLoad;
    
        //public ScriptObject[] _scriptObject;

        //Get Components
        private void Start()
        {
            audioPlayer = GetComponent<AudioSource>();
            itemName.text = itemScrub[ItemObjectScript.CurrentObjectInt].itemName;
            itemText.text = itemScrub[ItemObjectScript.CurrentObjectInt].itemText;
            itemImage.sprite = itemScrub[ItemObjectScript.CurrentObjectInt].itemImage;
            itemImage.transform.localScale = itemScrub[ItemObjectScript.CurrentObjectInt].itemSize;
            audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.CurrentObjectInt].itemAudio);
            _mainEventSystem = GameObject.FindWithTag("EventSystemMain");
            _levelLoaderObject = GameObject.FindWithTag("Level Loader");
            levelLoader = _levelLoaderObject.GetComponent<LevelLoader>();
        }

        //When Yes is Clicked, Play Cutscene
        public void OnClickYes()
        {
            Time.timeScale = 1;
            audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.CurrentObjectInt].cutSceneAudio);
            
            switch (ItemObjectScript.CurrentYesAnswer)
            {
                case 0: // Nothing happens
                    Debug.Log("Nothing");
                    timeline.Play();
                    _mainEventSystem.SetActive(true);
                    break;
                case 1: // Player goes to bed/couch, plays cutscene and then loads the next scene
                    crossFade.Play();
                    levelLoader.LoadNextLevelByIndex();
                    // Invoke(nameof(PlayNextScene),3f);
                    break;
                case 2: // Player fills the catBowl to full.
                    CatFoodFull.CatBowlFull = true;
                    break;
                case 3: // Load DeathScene by FrontDoor
                    evening3.Play();
                    Invoke(nameof(PlayDeathCredits),22f);
                    break;
            }
        }
    
        public void OnClickNo()
        {
            Time.timeScale = 1;
            Debug.Log("DeleteScene");
            ItemObjectScript.InItemCutscene = false;
            SceneManager.UnloadSceneAsync("InteractableItem");
        }

        private void PlayNextScene()
        {
            ItemObjectScript.InItemCutscene = false;
            SceneManager.LoadScene(_sceneToLoad);
        }

        private void PlayDeathCredits()
        {
            ItemObjectScript.InItemCutscene = false;
            levelLoader.LoadSceneByName("DeathCredits");
        }
        
    }
}
