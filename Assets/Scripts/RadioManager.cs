using System.Collections;
using PlayerScripts;
using Unity.VisualScripting;
using UnityEngine;

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
            StartCoroutine(PlayRadioMusic());  
        }
    }

    private void Update()
    {
        if (!UserInput.Interact || !_triggerActive)
            return;
        
        DataTransfer.TurnRadioOnOrOff();
        StartCoroutine(PlayRadioMusic());
    }
    
    private IEnumerator PlayRadioMusic()
    {
        if (!DataTransfer.RadioOn)
        {
            sfxSource.PlayOneShot(turnOffSfx);
            musicSource.Stop();
            yield break;
        }
        if (DataTransfer.RadioOn)
        {
            musicSource.PlayOneShot(radioMusic);
        }
        else if (DataTransfer.RadioOn && !musicSource.isPlaying && Time.deltaTime > 3f)
        {
            sfxSource.PlayOneShot(turnOnSfx);
        }

        yield return new WaitForSeconds(1.3f);
    } 
    private void OnTriggerEnter2D(Collider2D other) { _triggerActive = true; }
    private void OnTriggerExit2D(Collider2D other) { _triggerActive = false; }
}
