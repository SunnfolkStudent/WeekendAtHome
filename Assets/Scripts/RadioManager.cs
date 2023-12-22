using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class RadioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip turnOnSfx, turnOffSfx, radioMusic;
    
    private bool _triggerActive;
    
    void Start()
    {
        if (DataTransfer.RadioOn)
        {
            StartCoroutine(TurnRadioMusicOn(1f));
        }
    }

    private void Update()
    {
        if (!UserInput.Interact || !_triggerActive)
            return;
        {
            if (DataTransfer.RadioOn)
            {
                DataTransfer.TurnRadioOnOrOff();
                sfxSource.PlayOneShot(turnOffSfx);
                musicSource.Stop();
                StopAllCoroutines();
            }

            else if (!DataTransfer.RadioOn)
            {
                DataTransfer.TurnRadioOnOrOff();
                sfxSource.PlayOneShot(turnOnSfx);
                StartCoroutine(TurnRadioMusicOn(1f));
            }
            
        }

        /* if (radioOn && audioSource.volume < 1)
            StartCoroutine(TurnUpVolume());
        else if (!radioOn && audioSource.volume > 0)
            StartCoroutine(TurnDownVolume()); */
    }

    /* IEnumerator TurnUpVolume()
    {
        audioSource.volume += 0.5f * Time.deltaTime;
        yield return new WaitForSeconds(0.1f);
    }
    
    IEnumerator TurnDownVolume()
    {
        audioSource.volume -= 2f * Time.deltaTime;
        yield return new WaitForSeconds(0.1f);
    } */
    
    private void OnTriggerEnter2D(Collider2D other) { _triggerActive = true; }
    private void OnTriggerExit2D(Collider2D other) { _triggerActive = false; }

    private IEnumerator TurnRadioMusicOn(float startTime)
    {
        yield return new WaitForSecondsRealtime(1);
        musicSource.PlayOneShot(radioMusic);
        yield return new WaitForSecondsRealtime(startTime);
    }
    /* public void TurnOnOrOffRadio()
    {
        if (!radioOn) { sfxSource.PlayOneShot(turnOnSfx); }
        else
        {
            musicSource.Stop();
            sfxSource.PlayOneShot(turnOffSfx);
            radioOn = false;
        }
    } */
}
