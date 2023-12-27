using PlayerScripts;
using UnityEngine;

public class DataTransfer : MonoBehaviour
{
    private static DataTransfer _instance;
    
    // Set these to true / false inside here during testing.
    
    // PlayerCanMove should be true from the start.
    
    public static bool LampOn;
    public static bool TvOn; 
    public static bool RadioOn;
    public static bool GlassDoorOpen;
    public static bool PlayerCanMove = true;
    public static bool OnTopFloor;
    public static bool PlayerInside = true;
    public static int PlayerSortingOrder = 50;
    public static int VFXSortingOrder = 5;
    
    //Awake is always called before any Start functions
    private void Awake()
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
        TvOn = false;
        LampOn = false;
        if (PlayerInside)
        {
            PlayerSortingOrder = 50;
        }
        if (!PlayerInside)
        {
            PlayerSortingOrder = -1;
        }
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
    public static void SwitchFloors()
    {
        if (OnTopFloor)
        {
            OnTopFloor = false;
        }
        else if (!OnTopFloor)
        {
            OnTopFloor = true;
        }
    }
    public static void PlayerInsideOrOutside()
    {
        if (PlayerInside)
        {
            PlayerSortingOrder = 2;
            VFXSortingOrder = 30;
            PlayerInside = false;
        }
        else if (!PlayerInside)
        {
            PlayerSortingOrder = 50;
            VFXSortingOrder = 5;
            PlayerInside = true;
        }
    }
}
