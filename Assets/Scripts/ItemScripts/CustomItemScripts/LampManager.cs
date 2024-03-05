using System.Collections;
using PlayerScripts;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ItemScripts.CustomItemScripts
{
    public class LampManager : MonoBehaviour
    {
        [SerializeField] private Light2D lampLight;
    
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip lampOnSfx, lampOffSfx;
    
        private bool _triggerActive;

        private void Start()
        {
            lampLight = GetComponent<Light2D>();

            if (DataTransfer.lampOn)
            {
                lampLight.enabled = true;
            }
            else
            {
                lampLight.enabled = false;
            }
            // StartCoroutine(NewScene());
        }
        
        private IEnumerator NewScene()
        {
            yield return null;
            lampLight.enabled = DataTransfer.lampOn;
        }

        private IEnumerator PlayerNearLamp()
        {
            yield return new WaitUntil(() => !ItemObjectScript.inItemScene && UserInput.Interact || !_triggerActive);
            if (!_triggerActive) yield break;
            DataTransfer.TurnLampOnOrOff();
            yield return null;
            if (DataTransfer.lampOn)
            {
                audioSource.PlayOneShot(lampOnSfx);
                // lampLight.intensity = 1;
                lampLight.enabled = true; 
            }
            else if (!DataTransfer.lampOn)
            {
                audioSource.PlayOneShot(lampOffSfx);
                // lampLight.intensity = 0;
                lampLight.enabled = false;
            }
            yield return PlayerNearLamp();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return; 
            _triggerActive = true;
            StartCoroutine(PlayerNearLamp());
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return; 
            _triggerActive = false;
        }
    }
}
