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
    [SerializeField] private GameObject player;
    [SerializeField] private DataTransfer dataTransfer;
    [SerializeField] private PlayerInput input;
    
    [SerializeField] private Light2D light2D;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip tvTurningOn, tvTurningOff;

    private float _timer;
    private bool _triggerActive;
    public bool tvOn;
    
    // Start is called before the first frame update
    private void Start()
    {
        light2D = GetComponent<Light2D>();
        input = player.GetComponentInChildren<PlayerInput>();
        dataTransfer = dataTransfer.GetComponent<DataTransfer>();
        tvOn = dataTransfer.tvOn;
        // StartCoroutine(StartOrStopTvLightSwitch(0.04f));
    }
    
    private void OnTriggerEnter2D(Collider2D other) { _triggerActive = true; }
    private void OnTriggerExit2D(Collider2D other) { _triggerActive = false; }

    private void Update()
    {
        tvOn = dataTransfer.tvOn;
        _timer += Time.deltaTime;
        
        if (_timer > 12f)
        {
           StartCoroutine(StartOrStopTvLightSwitch(0.04f));
           _timer = -Time.deltaTime;
        }
        
        if (!input.interact || !_triggerActive)
            return;
        
        if (tvOn)
        {
            tvOn = false;
            audioSource.PlayOneShot(tvTurningOff);
            light2D.enabled = false;
        }
        else if (!tvOn)
        {
            tvOn = true;
            audioSource.PlayOneShot(tvTurningOn);
            light2D.enabled = true;
        }
        dataTransfer.TurnTVOnOrOff();
    }
    
    private IEnumerator StartOrStopTvLightSwitch(float checkTime)
    {
        if (tvOn)
        {
            StartCoroutine(ChangeLightAndWait(2f));
        }
        yield return new WaitForSeconds(checkTime);
    }

    private IEnumerator ChangeLightAndWait(float waitTime)
    {
        light2D.intensity = 0.2f;
        light2D.shapeLightFalloffSize = 0.8f;
        light2D.color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255), 0.02f);
        yield return new WaitForSeconds(waitTime);
        yield return StartOrStopTvLightSwitch(0.1f);
    }
    
}
