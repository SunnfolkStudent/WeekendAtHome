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
    //Should Get From Another Script

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        //itemName.text = itemScrub[ItemObjectScript.currentObjectInt].itemName;
        itemName.text = ItemObjectScript.currentObjectInt.ToString();
        itemText.text = itemScrub[ItemObjectScript.currentObjectInt].itemText;
        itemImage.sprite = itemScrub[ItemObjectScript.currentObjectInt].itemImage;
        audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.currentObjectInt].itemAudio);
    }

    public void OnClickYes()
    {
        timeline.Play();
        audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.currentObjectInt].cutSceneAudio);
    }

    public void OnClickNo()
    {
        Debug.Log("DeleteScene");
        SceneManager.UnloadSceneAsync("InteractableItem");
    }
}
