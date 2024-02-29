using System.Collections;
using PlayerScripts;
using UnityEngine;

namespace ItemScripts
{
    public class RadioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource turnOnSfxSource;
        [SerializeField] private AudioSource turnOffSfxSource;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioClip radioMusic;
    
        private bool _triggerActive;
        private void Start()
        {
            musicSource.clip = radioMusic;
            musicSource.Play(0);
            if (!musicSource.loop)
            {
                musicSource.loop = true;
            }
            if (!DataTransfer.radioOn)
            {
                musicSource.mute = true;
            }
        }

        private void Update()
        {
            if (!UserInput.Interact || !_triggerActive)
                return;
        
            // Debug.Log("TurnOnSFX length" + turnOnSfx.length);
            // Debug.Log("TurnOffSFX length" + turnOffSfx.length);
        
            RadioIsBeingInteractedWith();
        }

        private void RadioIsBeingInteractedWith()
        {
            if (DataTransfer.radioOn)
            {
                // Debug.Log("Radio on and being interacted with");
                turnOffSfxSource.Play(0);
                StartCoroutine(MuteRadio(0.30f));
            }
            else if (!DataTransfer.radioOn)
            {
                // Debug.Log("Radio off and being interacted with");
                turnOnSfxSource.Play(0);
                StartCoroutine(UnmuteRadio(1.8f));
            }
            DataTransfer.TurnRadioOnOrOff();
        }
    
        private IEnumerator UnmuteRadio(float delay)
        {
            yield return new WaitForSeconds(delay);
            // Debug.Log("Radio music is playing");
            // TODO: Instead of volume going straight to max, make it so the volume gradually is increased.
            musicSource.mute = false;
        } 
    
        private IEnumerator MuteRadio(float delay)
        {
            yield return new WaitForSeconds(delay);
            // Debug.Log("Radio music is muted");
            musicSource.mute = true;
        } 
        private void OnTriggerEnter2D(Collider2D other) { _triggerActive = true; }
        private void OnTriggerExit2D(Collider2D other) { _triggerActive = false; }
    }
}
