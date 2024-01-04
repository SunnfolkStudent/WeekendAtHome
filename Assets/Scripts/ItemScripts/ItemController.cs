using PlayerScripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ItemScripts
{
    public class ItemController : MonoBehaviour
    {
        public ItemType[] itemScrub;
        public TMP_Text itemName;
        public TMP_Text itemText;
        public Image itemImage;
        public AudioSource audioPlayer;
        private UserInput _input;

        //Sets Scrub To Scene
        private void Start()
        {
            itemName.text = itemScrub[ItemObjectScript.CurrentObjectInt].itemName;
            itemText.text = itemScrub[ItemObjectScript.CurrentObjectInt].itemText;
            itemImage.sprite = itemScrub[ItemObjectScript.CurrentObjectInt].itemImage;
            itemImage.transform.localScale = itemScrub[ItemObjectScript.CurrentObjectInt].itemSize;
            audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.CurrentObjectInt].itemAudio);

        }

        //Exits Scene if Interact is pressed
        private void Update()
        {
            if (UserInput.Interact)
            {
                Time.timeScale = 1;
                ItemObjectScript.InItemCutscene = false;
                SceneManager.UnloadSceneAsync("Item");
            }
        }
    }
}
