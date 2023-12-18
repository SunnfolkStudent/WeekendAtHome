using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemObjectScript : MonoBehaviour
{
    //Variables for what object should load
    [Header("What Scrub It Gets From The Scene")]
    public int thisObjectInt;
    
    [Header("Item Properties")]
    public bool isInteractable;
    public bool canNotInteractMultiple;
    public bool autoInteract;
    
    [Header("Disables Item")]
    public bool alreadyUsed;
    
    public static Vector2 currentObjectSize;
    public static int CurrentObjectInt;
    public static bool InItemCutscene;
    public static int CurrentYesAnswer;
    
    private bool _playerIsInTrigger;

    [Header("What Happens On Yes:")]
    [Header("0Nothing,1BedToTwo,2BedToThree,3BedToEnd,4CatFoodFull,5Couch1,6Couch2,7CatDead")]
    public int whatHappensOnYes;

    private void Start()
    {
        InItemCutscene = false;
    }

    //If in trigger load object.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsInTrigger = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsInTrigger = false;
        }
    }
    

    //If E is clicked when near, open the correct scene
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) && _playerIsInTrigger && !InItemCutscene && (!alreadyUsed || !canNotInteractMultiple)) || (autoInteract && _playerIsInTrigger && !InItemCutscene && (!alreadyUsed || !canNotInteractMultiple)))
        {
            Time.timeScale = 0;
            if (autoInteract && !canNotInteractMultiple)
            {
                _playerIsInTrigger = false;
            }

            CurrentYesAnswer = whatHappensOnYes;
            CurrentObjectInt = thisObjectInt;
            alreadyUsed = true;
            if (isInteractable)
            {
                InItemCutscene = true;
                SceneManager.LoadScene("InteractableItem",LoadSceneMode.Additive);
            }
            else
            {
                InItemCutscene = true;
                SceneManager.LoadScene("Item",LoadSceneMode.Additive);
            }
        }
    }
}
