using ItemScripts.CustomItemScripts;
using PlayerScripts;
using UnityEngine;

public class Radio : MonoBehaviour
{
    private BackgroundMusic _backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        _backgroundMusic = GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<BackgroundMusic>();
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (UserInput.Interact)
        {
            _backgroundMusic.RadioIsBeingInteractedWith();
        }
    }
}
