using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TVManager : MonoBehaviour
{
    [SerializeField] private Light2D light2D;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip tvTurningOn, tvTurningOff;

    private float _timer;
    private bool _triggerActive;
    
    // Start is called before the first frame update
    private void Start()
    {
        light2D = GetComponent<Light2D>();
        
        if (DataTransfer.TvOn)
        {
            if (!light2D.enabled)
            {
                light2D.enabled = true;
            }
            StartCoroutine(ChangeLightAndWait(2.5f));
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) { _triggerActive = true; }
    private void OnTriggerExit2D(Collider2D other) { _triggerActive = false; }

    private void Update()
    {
        if (!UserInput.Interact || !_triggerActive)
            return;
        {
            if (DataTransfer.TvOn)
            {
                DataTransfer.TurnTVOnOrOff();
                audioSource.PlayOneShot(tvTurningOff);
                light2D.enabled = false;
            }
            else if (!DataTransfer.TvOn)
            {
                DataTransfer.TurnTVOnOrOff();
                audioSource.PlayOneShot(tvTurningOn);
                light2D.enabled = true;
                // the float below in the ChangeLightAndWait is the initial start-up time before it starts changing colours (if 0f, it will blink very often)
                StartCoroutine(ChangeLightAndWait(2.5f));
            }
        }
    }
    
    /* private IEnumerator StartOrStopTvLightSwitch(float checkTime)
    {
        if (tvOn)
        {
            // The float inside ChangeLightAndWait determines how quickly the TvLight switches
            StartCoroutine(ChangeLightAndWait(1.5f));
        }
        yield return new WaitForSeconds(checkTime);
    } */

    private IEnumerator ChangeLightAndWait(float waitTime)
    {
        if (!DataTransfer.TvOn)
        {
            yield break;
        }
        light2D.intensity = 0.2f;
        light2D.shapeLightFalloffSize = 0.8f;
        light2D.color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255), 0.02f);
        yield return new WaitForSeconds(waitTime);
        yield return ChangeLightAndWait(2.5f);
    }
    
}
