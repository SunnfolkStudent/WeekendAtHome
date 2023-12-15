using System.Collections;
using System.Collections.Generic;
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

    //Get Components
    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        itemName.text = itemScrub[ItemObjectScript.currentObjectInt].itemName;
        //itemName.text = ItemObjectScript.currentObjectInt.ToString();
        itemText.text = itemScrub[ItemObjectScript.currentObjectInt].itemText;
        itemImage.sprite = itemScrub[ItemObjectScript.currentObjectInt].itemImage;
        itemImage.transform.localScale = itemScrub[ItemObjectScript.currentObjectInt].itemSize;
        audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.currentObjectInt].itemAudio);
    }

    //When Yes is Clicked, Play Cutscene
    public void OnClickYes()
    {
        timeline.Play();
        audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.currentObjectInt].cutSceneAudio);
    }

    //On No Leave Scene
    public void OnClickNo()
    {
        Debug.Log("DeleteScene");
        SceneManager.UnloadSceneAsync("InteractableItem");
    }
}
