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
    
    private bool _triggerActive;
    private Animator _animator;
    
    private AudioSource _audioSource;
    [SerializeField] private AudioClip doorClosing, doorOpening;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        if (DataTransfer.GlassDoorOpen)
        {
            _animator.Play("GlassDoorOpen");
            Debug.Log("GlassDoor Open");
        }
        else
        {
            _animator.Play("GlassDoorClosed");
            Debug.Log("GlassDoor Closed");
        }
    }

    private void Update()
    {
        slideTimer += Time.deltaTime;
        
        if (!_triggerActive)
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
                _animator.Play("GlassDoorOpening");
                _audioSource.PlayOneShot(doorOpening);
                break;
            case true:
                Debug.Log("DoorIsClosing");
                _animator.Play("GlassDoorClosing");
                _audioSource.PlayOneShot(doorClosing);
                break;
        }
        DataTransfer.OpenOrCloseGlassDoor();
        
        // Reset the timer to be 0.
        slideTimer = -Time.deltaTime;
        
        // Return controls back to the player once animation ends.
        /* if (!playerCanMove && slideTimer > 2.25f)
        {
            playerCanMove = true;
        } */
    }

    private void OnTriggerEnter2D(Collider2D other) { _triggerActive = true; }
    private void OnTriggerExit2D(Collider2D other) { _triggerActive = false; }

    
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
