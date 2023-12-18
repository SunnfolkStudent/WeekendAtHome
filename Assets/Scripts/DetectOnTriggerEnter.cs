using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class DetectOnTriggerEnter : MonoBehaviour
{
    public GameObject triggerbox;
    public bool destroySelf;
    
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        startEvent.Invoke();
        if (destroySelf)
        {
            Destroy(triggerbox);
        }
    }
}
