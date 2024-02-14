using TMPro;
using UnityEngine;
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

        //Sets Scrub To Scene
        private void Start()
        {
            itemName.text = itemScrub[ItemObjectScript.currentObjectInt].itemName;
            itemText.text = itemScrub[ItemObjectScript.currentObjectInt].itemText;
            itemImage.sprite = itemScrub[ItemObjectScript.currentObjectInt].itemImage;
            itemImage.transform.localScale = itemScrub[ItemObjectScript.currentObjectInt].itemSize;
            audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.currentObjectInt].itemAudio);
        }
    }
}
