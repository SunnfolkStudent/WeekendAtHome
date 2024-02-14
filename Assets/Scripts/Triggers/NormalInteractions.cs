using System;
using UnityEngine;
using UnityEngine.Events;

namespace Triggers
{
    public class NormalInteractions : InteractableBase
    {
        [Serializable] public class StartEvent : UnityEvent { }
        
        [SerializeField] private StartEvent startEvent = new StartEvent();

        public StartEvent OnStartEvent
        {
            get { return startEvent; }
            set { startEvent = value; }
        }
        protected override void Interact()
        {
            startEvent.Invoke();
        }
    }
}
