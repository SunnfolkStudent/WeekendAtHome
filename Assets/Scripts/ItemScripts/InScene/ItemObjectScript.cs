using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemObjectScript : MonoBehaviour
{
    public static Vector2 currentObjectSize;
    public static int currentObjectInt;
    public bool isCustom;
    public string customSceneToLoad;
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
    

    //If E is clicked when near, open the correct scene
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerIsInTrigger)
        {
            Time.timeScale = 0;
            currentObjectInt = thisObjectInt;
            alreadyUsed = true;
            _playerIsInTrigger = false;
            if(isCustom)
            {
                SceneManager.LoadScene(customSceneToLoad,LoadSceneMode.Additive);
            }
            else if (isInteractable)
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
