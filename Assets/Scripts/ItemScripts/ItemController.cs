using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
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
    private PlayerInput _input;

    //Sets Scrub To Scene
    private void Start()
    {
        itemName.text = itemScrub[ItemObjectScript.currentObjectInt].itemName;
        itemText.text = itemScrub[ItemObjectScript.currentObjectInt].itemText;
        itemImage.sprite = itemScrub[ItemObjectScript.currentObjectInt].itemImage;
        itemImage.transform.localScale = itemScrub[ItemObjectScript.currentObjectInt].itemSize;
        audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.currentObjectInt].itemAudio);

    }

    //Exits Scene if E is pressed
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.UnloadSceneAsync("Item");
        }
    }
}
