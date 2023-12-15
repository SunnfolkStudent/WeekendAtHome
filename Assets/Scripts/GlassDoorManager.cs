using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassDoorManager : MonoBehaviour
{

    private Animator animator;
    public bool isOpen;

    public bool isPlaying;

    private AudioSource audioSource;
    [SerializeField] private AudioClip doorClosing, doorOpening;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isOpen = false;
    }

    public void OpenOrCloseDoor()
    {
        if (isPlaying)
            return;

        if (!isOpen)
        {
            animator.Play("GlassDoorSliding");
            audioSource.PlayOneShot(doorOpening);
        }
        else
        {
            animator.Play("GlassDoorSlidingClosed");
            audioSource.PlayOneShot(doorClosing);
        }

        isOpen = !isOpen;
        
        return;
    }

    public void SetAnimationToPlayingOrNot() { isPlaying = !isPlaying; }

    public void AllowMovementAgain() { GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().canMove = true; }
}
