using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CatFoodScript : MonoBehaviour
{
    public ItemType[] itemScrub;
    public TMP_Text itemName;
    public TMP_Text itemText;
    public Image itemImage;
    public AudioSource audioPlayer;
    public PlayableDirector timeline;
    public static bool catFoodFull;
    //Should Get From Another Script

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        itemName.text = itemScrub[ItemObjectScript.currentObjectInt].itemName;
        //itemName.text = ItemObjectScript.currentObjectInt.ToString();
        audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.currentObjectInt].itemAudio);

        if (catFoodFull)
        {
            itemText.text = itemScrub[ItemObjectScript.currentObjectInt].itemText;
            itemImage.sprite = itemScrub[ItemObjectScript.currentObjectInt].itemImage;
        }
    }

    public void OnClickYes()
    {
        catFoodFull = true;
        timeline.Play();
        audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.currentObjectInt].cutSceneAudio);
    }

    public void OnClickNo()
    {
        Debug.Log("DeleteScene");
        SceneManager.UnloadSceneAsync("InteractableItem");
    }
}