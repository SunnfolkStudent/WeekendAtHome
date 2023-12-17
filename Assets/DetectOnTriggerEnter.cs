using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectOnTriggerEnter : MonoBehaviour
{
    [Serializable]
    public class StartEvent : UnityEvent
    {
        
    }

    [SerializeField] private StartEvent startEvent = new StartEvent();

    public StartEvent OnStartEvent
    {
        get { return startEvent; }
        set { startEvent = value; }
    }

    private void OnTriggerEnter2D()
    {
        startEvent.Invoke();
    }
}
