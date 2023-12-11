using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemController : MonoBehaviour
{
    public ItemType[] itemScrub;
    public TMP_Text itemName;
    public TMP_Text itemText;
    public Image itemImage;
    public AudioSource audioPlayer;
    //Should Get From Another Script

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        itemName.text = itemScrub[ItemObjectScript.currentObjectInt].itemName;
        itemText.text = itemScrub[ItemObjectScript.currentObjectInt].itemText;
        itemImage.sprite = itemScrub[ItemObjectScript.currentObjectInt].itemImage;
        audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.currentObjectInt].itemAudio);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.UnloadSceneAsync("Item");
        }
    }
}
