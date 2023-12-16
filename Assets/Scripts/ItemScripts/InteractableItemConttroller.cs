using System.Collections;
using System.Collections.Generic;
using Inheritance;
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
        
        timeline.Play();
        audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.CurrentObjectInt].cutSceneAudio);
        switch (ItemObjectScript.CurrentYesAnswer)
        {
            case 0:
                Debug.Log("Nothing");
                
                break;
            case 1:
                Debug.Log("Fill Cat Food");
                //_scriptObject.GetComponent<ScriptObject>().GetMethod(int)
                
                break;
            case 2:
                Debug.Log("Close Catdoor");
                break;
            case 3:
                Debug.Log("start Sleep Scene");
                break;
        }
    }

    /*public override void Interact()
    {
        
    }*/

    //On No Leave Scene
    public void OnClickNo()
    {
        Debug.Log("DeleteScene");
        ItemObjectScript.InItemCutscene = false;
        SceneManager.UnloadSceneAsync("InteractableItem");
    }

    public void PlayMethod(int objectNumber)
    {
        
    }
}
