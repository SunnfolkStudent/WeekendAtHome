using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RadioManager : MonoBehaviour
{

    public bool isPlaying;
    public bool turnedOn;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip turnOnSound, turnOffSound, music;

    private bool canTurnOn;
    
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!canTurnOn)
            return;
        
        if (Keyboard.current.eKey.wasReleasedThisFrame)
            TurnOnOrOffRadio();
        
        if (turnedOn && !isPlaying)
        {
            isPlaying = true;
            audioSource.Play();
        }

        if (turnedOn && audioSource.volume < 1)
            StartCoroutine(TurnUpVolume());
        else if (!turnedOn && audioSource.volume > 0)
            StartCoroutine(TurnDownVolume());
    }

    IEnumerator TurnUpVolume()
    {
        audioSource.volume += 0.5f * Time.deltaTime;
        yield return new WaitForSeconds(0.1f);
    }
    
    IEnumerator TurnDownVolume()
    {
        audioSource.volume -= 2f * Time.deltaTime;
        yield return new WaitForSeconds(0.1f);
    }
    
    private void OnTriggerEnter2D(Collider2D other) { canTurnOn = true; }
    private void OnTriggerExit2D(Collider2D other) { canTurnOn = false; }

    
    public void TurnOnOrOffRadio()
    {

        if (!turnedOn) { audioSource.PlayOneShot(turnOnSound); }
        else
        {
            audioSource.Stop();
            isPlaying = false;
            audioSource.PlayOneShot(turnOffSound);
        }

        turnedOn = !turnedOn;
        
        return;
    }
}
