using PlayerScripts;
using Triggers;
using UnityEngine;
using UnityEngine.Rendering;

public class DataTransfer : MonoBehaviour
{
    private static DataTransfer _instance;
    
    /// <summary>
    /// Set the bools inside here to true / false inside here during testing.
    /// </summary>
    /// <returns></returns>
    public const int CatSortingOrderOutside = -1;
    
    public static bool LampOn;
    public static bool TvOn; 
    public static bool RadioOn;
    public static bool GlassDoorOpen;
    public static bool BedroomDoorOpen;
    public static bool PlayerCanMove = true; // PlayerCanMove should be true from the start.
    public static bool OnTopFloor = false; // Change this in Editor if you're changing starting floors.
    public static bool InsideBathroom;
    public static bool PlayerInside = true;
    public static bool CatOutside;
    public static int PlayerSortingOrder = 50;
    public static int CatSortingOrderInside = 50;
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
        
        if (PlayerInside && !OnTopFloor)
        {
            PlayerSortingOrder = 50;
            CatSortingOrderInside = 50;
        }
        else if (PlayerInside && OnTopFloor)
        {
            CatSortingOrderInside = -1;
        }
        else if (!PlayerInside)
        {
            PlayerSortingOrder = -1;
            CatSortingOrderInside = 50;
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

    public static void OpenOrCloseBedroomDoor()
    {
        if (BedroomDoorOpen)
        {
            BedroomDoorOpen = false;
        }
        else if (!BedroomDoorOpen)
        {
            BedroomDoorOpen = true;
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
            CatSortingOrderInside = 50;
            
        }
        else if (!OnTopFloor)
        {
            OnTopFloor = true;
            CatSortingOrderInside = -1;
        }
    }

    public static void EnterOrExitBathroom()
    {
        if (InsideBathroom)
        {
            InsideBathroom = false;
        }
        else
        {
            InsideBathroom = true;
        }
    }
    public static void PlayerInsideOrOutside()
    {
        if (PlayerInside)
        {
            PlayerSortingOrder = -1;
            VFXSortingOrder = 60;
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
