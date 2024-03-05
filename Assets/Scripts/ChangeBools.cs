using Triggers;
using UnityEngine;

public class ChangeBools : MonoBehaviour
{
    private StairsTrigger _stairsTrigger;
    public bool OnTopFloor; // Change this in Editor if you're changing starting floors.
    
    // Update is called once per frame
    void Start()
    {
        _stairsTrigger = GetComponentInParent<StairsTrigger>();
        if (OnTopFloor)
        {
            _stairsTrigger.StartingOnTopFloor();
        }
    }
}
