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
    public int current;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        itemName.text = itemScrub[current].itemName;
        itemText.text = itemScrub[current].itemText;
        itemImage.sprite = itemScrub[current].itemImage;
        audioPlayer.PlayOneShot(itemScrub[current].itemAudio);
    }

    public void OnClickYes()
    {
        timeline.Play();
        audioPlayer.PlayOneShot(itemScrub[current].cutSceneAudio);
    }

    public void OnClickNo()
    {
        Debug.Log("DeleteScene");
        SceneManager.UnloadSceneAsync("InteractableItem");
    }
}
