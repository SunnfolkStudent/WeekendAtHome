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
        // TODO: Make this script run the needed functions in the mainScene LevelLoader through methods from another script.
        // TODO: Make it use the same EventSystem as in the mainScene
        // TODO: Refactor the functions, splitting them up or using functions in other scripts.
        
        public ItemType[] itemScrub;
        public TMP_Text itemName;
        public TMP_Text itemText;
        public Image itemImage;
        public AudioSource audioPlayer;

        private string _sceneToLoad;

        //Get Components
        private void Start()
        {
            audioPlayer = GetComponent<AudioSource>();
            itemName.text = itemScrub[ItemObjectScript.currentObjectInt].itemName;
            itemText.text = itemScrub[ItemObjectScript.currentObjectInt].itemText;
            itemImage.sprite = itemScrub[ItemObjectScript.currentObjectInt].itemImage;
            itemImage.transform.localScale = itemScrub[ItemObjectScript.currentObjectInt].itemSize;
            audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.currentObjectInt].itemAudio);
        }

        //When Yes is Clicked, Play Cutscene
        public void OnClickYes()
        {
            Time.timeScale = 1;
            audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.currentObjectInt].cutSceneAudio);
            
            switch (ItemObjectScript.currentYesAnswer)
            {
                case 0: // Nothing happens
                    Debug.Log("Nothing");
                    SceneManager.UnloadSceneAsync("Item");
                    break;
                case 1: // Player goes to bed/couch, plays cutscene and then loads the next scene
                    // Invoke(nameof(PlayNextScene),3f);
                    break;
                case 2: // Player fills the catBowl to full.
                    CatFoodFull.CatBowlFull = true;
                    break;
                case 3: // Load DeathScene by FrontDoor
                    Invoke(nameof(PlayDeathCredits),22f);
                    break;
            }
        }
    
        public void OnClickNo()
        {
            Time.timeScale = 1;
            Debug.Log("DeleteScene");
            ItemObjectScript.inItemCutscene = false;
            SceneManager.UnloadSceneAsync("InteractableItem");
        }

        private void PlayNextScene()
        {
            ItemObjectScript.inItemCutscene = false;
            SceneManager.LoadScene(_sceneToLoad);
        }

        private void PlayDeathCredits()
        {
            ItemObjectScript.inItemCutscene = false;
        }
        
    }
}
