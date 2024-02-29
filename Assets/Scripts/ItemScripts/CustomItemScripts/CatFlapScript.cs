using System.Collections;
using PlayerScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace ItemScripts.CustomItemScripts
{
    public class CatFlapScript : MonoBehaviour
    {
        [SerializeField] private GameObject catFlapDoorObject;
        private BoxCollider2D _catFlapDoorTrigger;

        [SerializeField] private AudioSource catFlapAudioSource;
        [SerializeField] private AudioClip catFlapUnlock, catFlapLock;
    
        // Start is called before the first frame update
        void Awake()
        {
            catFlapDoorObject = GameObject.FindWithTag("CatFlapCollision");
            _catFlapDoorTrigger = GetComponent<BoxCollider2D>();
            
            if (DataTransfer.catFlapClosed)
            {
                catFlapDoorObject.SetActive(true);
            }
            else if (!DataTransfer.catFlapClosed)
            {
                catFlapDoorObject.SetActive(false);
            }
        }
    
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && UserInput.Interact)
            {
                DataTransfer.OpenOrCloseCatFlapDoor();
                
                if (DataTransfer.catFlapClosed)
                {
                    catFlapAudioSource.PlayOneShot(catFlapLock);
                    catFlapDoorObject.SetActive(true);
                    Debug.Log("Catflap is now locked");
                }
                else if (!DataTransfer.catFlapClosed)
                {
                    catFlapAudioSource.PlayOneShot(catFlapUnlock);
                    catFlapDoorObject.SetActive(false);
                    Debug.Log("Catflap is now unlocked");
                }
                StartCoroutine(UpdateCatPath());
            }
        }

        private IEnumerator UpdateCatPath()
        {
            yield return new WaitForSeconds(1f);
            AstarPath.active.UpdateGraphs(_catFlapDoorTrigger.bounds);
        }
    }
}
