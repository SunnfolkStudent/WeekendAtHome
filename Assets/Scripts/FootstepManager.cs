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
        audioSource.PlayOneShot(!DataTransfer.Inside ? snowyStep : indoorStep);
    }
}
