using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{

    private AudioSource audioSource;

    public AudioClip indoorStep, snowyStep;

    public bool isOutside;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playFootstep()
    {
        if (!isOutside)
            audioSource.PlayOneShot(indoorStep);
        else
            audioSource.PlayOneShot(snowyStep); 
    }
}
