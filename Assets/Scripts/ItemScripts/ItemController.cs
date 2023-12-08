using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public ItemType[] itemScrub;
    public TMP_Text itemName;
    public TMP_Text itemText;
    public Image itemImage;
    public AudioSource audioPlayer;
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
}
