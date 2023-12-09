using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenContinue : MonoBehaviour
{
    private PlayerControls controls;
    [SerializeField] private LevelLoader levelLoader;
    
    private void Awake() => controls = new PlayerControls();
    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();
    
    void Update()
    {
        if (controls.AnyButton.AnyKey.triggered)
            levelLoader.loadNextLevel();
    }
}
