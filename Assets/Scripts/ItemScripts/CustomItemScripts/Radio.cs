using PlayerScripts;
using UnityEngine;

namespace ItemScripts.CustomItemScripts
{
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
}
