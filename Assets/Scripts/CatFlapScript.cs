using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;

public class CatFlapScript : MonoBehaviour
{
    [SerializeField] private GameObject catFlapDoorCollision;

    [SerializeField] private AudioSource catFlapAudioSource;
    [SerializeField] private AudioClip catFlapUnlock, catFlapLock;
    
    // Start is called before the first frame update
    void Awake()
    {
        catFlapDoorCollision = GameObject.FindWithTag("CatFlapCollision");
        
        if (DataTransfer.CatFlapClosed)
        {
            catFlapDoorCollision.SetActive(true);
        }
        else if (!DataTransfer.CatFlapClosed)
        {
            catFlapDoorCollision.SetActive(false);
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && UserInput.Interact)
        {
            DataTransfer.OpenOrCloseCatFlapDoor();
                
            if (DataTransfer.CatFlapClosed)
            {
                catFlapAudioSource.PlayOneShot(catFlapLock);
                catFlapDoorCollision.SetActive(true);
                Debug.Log("Catflap is now locked");
            }
            else if (!DataTransfer.CatFlapClosed)
            {
                catFlapAudioSource.PlayOneShot(catFlapUnlock);
                catFlapDoorCollision.SetActive(false);
                Debug.Log("Catflap is now unlocked");
            }
            AstarPath.active.Scan();
        }
    }
}
