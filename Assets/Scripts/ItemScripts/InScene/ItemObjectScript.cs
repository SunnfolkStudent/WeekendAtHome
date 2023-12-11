using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemObjectScript : MonoBehaviour
{
    public static int currentObjectInt;
    public int thisObjectInt;
    public bool isInteractable;
    public bool alreadyUsed;

    private bool _playerIsInTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !alreadyUsed)
        {
            _playerIsInTrigger = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !alreadyUsed)
        {
            _playerIsInTrigger = false;
        }
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerIsInTrigger)
        {
            currentObjectInt = thisObjectInt;
            alreadyUsed = true;
            _playerIsInTrigger = false;
            if (isInteractable)
            {
                SceneManager.LoadScene("InteractableItem",LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.LoadScene("Item",LoadSceneMode.Additive);
            }
        }
    }
}
