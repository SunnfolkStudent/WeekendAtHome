using System.Collections;
using System.Collections.Generic;
using Inheritance;
using ItemScripts;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InteractableItemConttroller : MonoBehaviour
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
    public LevelLoader levelLoader;

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
    }

    //When Yes is Clicked, Play Cutscene
    public void OnClickYes()
    {
        Time.timeScale = 1;
        audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.CurrentObjectInt].cutSceneAudio);
        switch (ItemObjectScript.CurrentYesAnswer)
        {
            case 0:
                Debug.Log("Nothing");
                timeline.Play();
                break;
            case 1:
                _sceneToLoad = "Day 2 - Morning";
                crossFade.Play();
                Invoke("PlayNextScene",3f);
                break;
            case 2:
                _sceneToLoad = "Day 3 - Morning";
                crossFade.Play();
                Invoke("PlayNextScene",3f);
                break;
            case 3:
                break;
            case 4:
                CatFoodFull.catBowlFull = true;
                break;
            case 5:
                _sceneToLoad = "Day 2 - Evening";
                evening2.Play();
                Invoke("PlayNextScene",22f);
                break;
            case 6:
                _sceneToLoad = "Day 3 - Evening";
                evening3.Play();
                Invoke("PlayNextScene",22f);
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

    void PlayNextScene()
    {
        ItemObjectScript.InItemCutscene = false;
        SceneManager.LoadScene(_sceneToLoad);
    }
}
