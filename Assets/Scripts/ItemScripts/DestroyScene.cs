using System;
using System.Collections;
using System.Collections.Generic;
using ItemScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DestroyScene : MonoBehaviour
{
    private void OnEnable()
    {
        ItemObjectScript.inItemCutscene = false;
        Debug.Log("DeleteScene");
        SceneManager.UnloadSceneAsync("InteractableItem");
    }
}
