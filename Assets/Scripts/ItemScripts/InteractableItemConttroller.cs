using System.Collections;
using System.Collections.Generic;
using Inheritance;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InteractableItemConttroller : MonoBehaviour
{
    public ItemType[] itemScrub;
    public TMP_Text itemName;
    public TMP_Text itemText;
    public Image itemImage;
    public AudioSource audioPlayer;
    public PlayableDirector timeline;
    public GameObject catBowlFull;

    private string _sceneToLoad;
    
    //public ScriptObject[] _scriptObject;

    //Get Components
    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        itemName.text = itemScrub[ItemObjectScript.CurrentObjectInt].itemName;
        itemText.text = itemScrub[ItemObjectScript.CurrentObjectInt].itemText;
        itemImage.sprite = itemScrub[ItemObjectScript.CurrentObjectInt].itemImage;
        itemImage.transform.localScale = itemScrub[ItemObjectScript.CurrentObjectInt].itemSize;
        audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.CurrentObjectInt].itemAudio);
    }

    //When Yes is Clicked, Play Cutscene
    public void OnClickYes()
    {
        Time.timeScale = 1;
        timeline.Play();
        audioPlayer.PlayOneShot(itemScrub[ItemObjectScript.CurrentObjectInt].cutSceneAudio);
        switch (ItemObjectScript.CurrentYesAnswer)
        {
            case 0:
                Debug.Log("Nothing");
                break;
            case 1:
                SceneManager.LoadScene("Day 2 - Morning");
                _sceneToLoad = "Day 2 - Morning";
                StartCoroutine("NextDay");
                Debug.Log("LoadMorning2");
                //_scriptObject.GetComponent<ScriptObject>().GetMethod(int)
                
                break;
            case 2:
                _sceneToLoad = "Day 3 - Morning";
                Debug.Log("LoadMorning 3");
                break;
            case 3:
                _sceneToLoad = "EndScene";
                Debug.Log("EndGame");
                break;
            case 4:
                catBowlFull = GameObject.FindWithTag("CatBowlFull");
                catBowlFull.transform.position = new Vector3(8.417f, 7.561f, -0.1129886f);
                break;
                
        }
    }
    
    public void OnClickNo()
    {
        Time.timeScale = 1;
        Debug.Log("DeleteScene");
        ItemObjectScript.InItemCutscene = false;
        SceneManager.UnloadSceneAsync("InteractableItem");
    }

    public void PlayMethod(int objectNumber)
    {
        
    }

    private IEnumerator NextDay()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(_sceneToLoad);
    }
}
