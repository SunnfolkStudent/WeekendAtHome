using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private Keyboard keyboard;
    
    public bool interactKeyPressed;
    
    void Start()
    {
        keyboard = Keyboard.current;
    }

    
    void Update()
    {
        
    }
}
