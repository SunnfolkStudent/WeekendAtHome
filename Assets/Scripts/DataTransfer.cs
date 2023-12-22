using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using PlayerScripts;
using UnityEngine;
using UnityEngine.Serialization;

public class DataTransfer : MonoBehaviour
{
    private static DataTransfer _instance;
    [FormerlySerializedAs("playerInput")] [SerializeField] private UserInput userInput;

    [Header("Configurable Universal Bools")] 
    public static bool LampOn;
    public static bool TvOn; 
    public static bool RadioOn;
    public static bool GlassDoorOpen;
    public static bool PlayerCanMove;
    public static bool TopFloorElseBottomFloor;
    public static bool BottomFloorOrOutside;
    
    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (_instance != null && _instance != this)
        {
            Destroy(_instance);
            _instance = this;
        }
        //If instance already exists and it's not this:
        /* else if (Instance != this)
        {
            Destroy(gameObject);   
        } */
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        userInput = GetComponent<UserInput>();
    }

    public static void TurnLampOnOrOff()
    {
        if (LampOn)
        {
           LampOn = false;
        }
        else if (!LampOn)
        {
            LampOn = true;
        }
    }
    public static void TurnTVOnOrOff()
    {
        if (TvOn)
        {
            TvOn = false;
        }
        else if (!TvOn)
        {
            TvOn = true;
        }
    }
    public static void TurnRadioOnOrOff()
    {
        if (RadioOn)
        {
            RadioOn = false;
        }
        else if (!RadioOn)
        {
            RadioOn = true;
        }
    }
    public static void OpenOrCloseGlassDoor()
    {
        if (GlassDoorOpen)
        {
            GlassDoorOpen = false;
        }
        else if (!GlassDoorOpen)
        {
            GlassDoorOpen = true;
        }
    }
    public static void CanPlayerMove(UserInput userInput)
    {
        if (PlayerCanMove)
        {
            PlayerCanMove = false;
            userInput.OnDisable();
        }
        else if (!PlayerCanMove)
        {
            PlayerCanMove = true;
            userInput.OnEnable();
        }
    }

    public static void TopOrBottomFloor()
    {
        if (TopFloorElseBottomFloor)
        {
            TopFloorElseBottomFloor = false;
        }
        else if (!TopFloorElseBottomFloor)
        {
            TopFloorElseBottomFloor = true;
        }
    }

    public static void MoveInsideOrOutside()
    {
        if (BottomFloorOrOutside)
        {
            BottomFloorOrOutside = false;
        }
        else if (!BottomFloorOrOutside)
        {
            BottomFloorOrOutside = true;
        }
    }
    
}
