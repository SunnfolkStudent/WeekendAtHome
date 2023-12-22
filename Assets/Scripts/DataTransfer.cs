using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class DataTransfer : MonoBehaviour
{
    private static DataTransfer _instance;

    [Header("Configurable Parameters")] 
    public bool lampOn;
    public bool tvOn; 
    public bool radioOn;
    public bool glassDoorOpen;
    public bool playerCanMove;
    public bool TopFloorElseBottomFloor;
    public bool BottomFloorElseOutside;
    
    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (_instance == null)
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

    public void TurnLampOnOrOff()
    {
        if (lampOn)
        {
           lampOn = false;
        }
        else if (!lampOn)
        {
            lampOn = true;
        }
    }
    public void TurnTVOnOrOff()
    {
        if (tvOn)
        {
            tvOn = false;
        }
        else if (!tvOn)
        {
            tvOn = true;
        }
    }
    public void TurnRadioOnOrOff()
    {
        if (radioOn)
        {
            radioOn = false;
        }
        else if (!radioOn)
        {
            radioOn = true;
        }
    }
    public void OpenOrCloseGlassDoor()
    {
        if (glassDoorOpen)
        {
            glassDoorOpen = false;
        }
        else if (!glassDoorOpen)
        {
            glassDoorOpen = true;
        }
    }
    public void CanPlayerMove()
    {
        if (playerCanMove)
        {
            playerCanMove = false;
        }
        else if (!playerCanMove)
        {
            playerCanMove = true;
        }
    }

    public void TopOrBottomFloor()
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

    public void InsideOrOutside()
    {
        if (BottomFloorElseOutside)
        {
            BottomFloorElseOutside = false;
        }
        else if (!BottomFloorElseOutside)
        {
            BottomFloorElseOutside = true;
        }
    }
    
}
