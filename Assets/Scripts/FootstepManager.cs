using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public AudioClip indoorStep, snowyStep;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void PlayFootstep()
    {
        if (!DataTransfer.BottomFloorOrOutside)
            audioSource.PlayOneShot(snowyStep);
        else
            audioSource.PlayOneShot(indoorStep); 
    }
}
