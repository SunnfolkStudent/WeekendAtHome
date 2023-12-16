using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DestroyScene : MonoBehaviour
{
    private void OnEnable()
    {
        ItemObjectScript.InItemCutscene = false;
        Debug.Log("DeleteScene");
        SceneManager.UnloadSceneAsync("InteractableItem");
    }
}
