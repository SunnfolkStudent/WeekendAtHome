using System.Collections;
using PlayerScripts;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class TVManager : MonoBehaviour
{
    [SerializeField] private Light2D light2D;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip tvTurningOn, tvTurningOff;

    private float _timer;
    private bool _triggerActive;
    
    // Start is called before the first frame update
    private void Start()
    {
        light2D = GetComponent<Light2D>();
    }
    private void OnTriggerEnter2D(Collider2D other) { _triggerActive = true; } 
    private void OnTriggerExit2D(Collider2D other) { _triggerActive = false; }

    private void Update()
    {
        if (!UserInput.Interact || !_triggerActive) 
            return;
        
        DataTransfer.TurnTVOnOrOff();
        // the float below in the ChangeLightAndWait is the initial start-up time before it starts changing colours (if 0f, it will blink very often)
        StartCoroutine(ChangeLightAndWait(1f));
            
        if (DataTransfer.tvOn)
        {
            audioSource.PlayOneShot(tvTurningOn);
            light2D.enabled = true; 
        }
        else if (!DataTransfer.tvOn)
        {
            audioSource.PlayOneShot(tvTurningOff);
            light2D.enabled = false;
        }
    }
    private IEnumerator ChangeLightAndWait(float waitTime)
    {
        if (!DataTransfer.tvOn)
        {
            yield break;
        }
        light2D.intensity = 0.25f;
        light2D.shapeLightFalloffSize = 0.8f;
        light2D.color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255), 0.02f);
        yield return new WaitForSeconds(waitTime);
        yield return ChangeLightAndWait(2f);
    }
}
