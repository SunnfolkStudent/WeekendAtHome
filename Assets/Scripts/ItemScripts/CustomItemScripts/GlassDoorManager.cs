using System.Collections;
using PlayerScripts;
using UnityEngine;

namespace ItemScripts.CustomItemScripts
{
    public class GlassDoorManager : MonoBehaviour
    {
        private Animator _animator;
        private BoxCollider2D _collider2D;
        private AudioSource _audioSource;
        [SerializeField] private AudioClip doorClosing, doorOpening;
        
        private bool _triggerActive;
        private float _slideTimer;
        void Start()
        {
            _collider2D = GetComponent<BoxCollider2D>();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();

            _animator.Play(DataTransfer.glassDoorOpen ? "GlassDoorOpen" : "GlassDoorClosed");
        }

        private void Update()
        {
            _slideTimer += Time.deltaTime;
        
            if (!_triggerActive)
                return;
        
            // if animation is done, and player is within triggerBox and presses Interact, continue downwards.
            if (!(_slideTimer > 2.25f) || !UserInput.Interact) 
                return;
            
            AstarPath.active.UpdateGraphs(_collider2D.bounds); 
            
            // If glassDoor is Open, close it. If glassDoor is Closed, open it.
            switch (DataTransfer.glassDoorOpen)
            {
                case false:
                    _animator.Play("GlassDoorOpening");
                    _audioSource.PlayOneShot(doorOpening);
                    StartCoroutine(UpdateCatPath(true));
                    break;
                case true:
                    _animator.Play("GlassDoorClosing");
                    _audioSource.PlayOneShot(doorClosing);
                    StartCoroutine(UpdateCatPath(false));
                    break;
            }
            DataTransfer.OpenOrCloseGlassDoor();
            
            _slideTimer = -Time.deltaTime;
        }

        private IEnumerator UpdateCatPath(bool doorIsOpening)
        {
            if (doorIsOpening)
            {
                yield return new WaitForSeconds(2.25f);
            }
            else
            {
                yield return new WaitForSeconds(1.5f);
            }
            AstarPath.active.UpdateGraphs(_collider2D.bounds); 
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == null || !collision.CompareTag("Player")) { return; }
        
            _triggerActive = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision == null || !collision.CompareTag("Player")) { return; }
        
            _triggerActive = false; 
        }
    }
}
