using System;
using System.Collections;
using System.Collections.Generic;
using PlayerScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GlassDoorManager : MonoBehaviour
{
    [SerializeField] private float slideTimer;
    
    public bool triggerActive;
    private Animator _animator;
    
    private AudioSource _audioSource;
    [SerializeField] private AudioClip doorClosing, doorOpening;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        slideTimer += Time.deltaTime;
        
        if (!triggerActive)
            return;
        
        // if animation is done, and player is within triggerBox and presses Interact, continue downwards.
        if (!(slideTimer > 2.25f) || !UserInput.Interact) 
            return;
        
        // Disable userInput while glassDoor opens
        // dataTransfer.playerCanMove = false;
            
        // If glassDoor is Open, close it. If glassDoor is Closed, open it.
        switch (DataTransfer.GlassDoorOpen)
        {
            case false:
                Debug.Log("DoorIsOpening");
                _animator.Play("GlassDoorSliding");
                _audioSource.PlayOneShot(doorOpening);
                DataTransfer.GlassDoorOpen = true;
                break;
            case true:
                Debug.Log("DoorIsClosing");
                _animator.Play("GlassDoorSlidingClosed");
                _audioSource.PlayOneShot(doorClosing);
                DataTransfer.GlassDoorOpen = false;
                break;
        }
        // Reset the timer to be 0.
        slideTimer = -Time.deltaTime;
        
        // Return controls back to the player once animation ends.
        /* if (!playerCanMove && slideTimer > 2.25f)
        {
            playerCanMove = true;
        } */
    }

    private void OnTriggerEnter2D(Collider2D other) { triggerActive = true; }
    private void OnTriggerExit2D(Collider2D other) { triggerActive = false; }

    
    /* public void SetAnimationToPlayingOrNot()
    {
        isPlaying = !isPlaying;
    }

    public void AllowMovementAgain()
    {
        oops.PlayerCanMove = true;
    }

    public void OpenOrCloseDoor()
    {
        if (isPlaying)
        {
            _input.OnDisable();
            Debug.Log("DoorIsMoving");
            return;
        }

        if (!GlassDoorOpen && triggerActive && _input.interact)
        {
            Debug.Log("DoorIsOpening");
            _animator.Play("GlassDoorSliding");
            _audioSource.PlayOneShot(doorOpening);
            SetAnimationToPlayingOrNot();
        }
        else if (GlassDoorOpen && triggerActive && _input.interact)
        {
            Debug.Log("DoorIsClosing");
            _animator.Play("GlassDoorSlidingClosed");
            _audioSource.PlayOneShot(doorClosing);
            SetAnimationToPlayingOrNot();
        }

        GlassDoorOpen = !GlassDoorOpen;
        _input.OnEnable();
    } */
}
