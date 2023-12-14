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
    //Should Get From Another Script
    
    /*private void Awake() => _input = new PlayerInput();
    private void OnEnable() => _input.Enable();
    private void OnDisable() => _input.Disable();*/

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        itemName.text = itemScrub[ItemObjectScript.currentObjectInt].itemName;
        itemText.text = itemScrub[ItemObjectScript.currentObjectInt].itemText;
        itemImage.sprite = itemScrub[ItemObjectScript.currentObjectInt].itemImage;
        itemImage.transform.localScale = itemScrub[ItemObjectScript.currentObjectInt].itemSize;
        audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.currentObjectInt].itemAudio);
    }

    private void Update()
    {
        /*if (_input.interact)
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("Item");
        }*/
    }
}
