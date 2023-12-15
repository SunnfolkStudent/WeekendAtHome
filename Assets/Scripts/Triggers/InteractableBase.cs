using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Inheritance
{
    public abstract class InteractableBase : MonoBehaviour
    {
        private bool interactable;

        private void Update()
        {
            if (interactable &&  Keyboard.current.eKey.wasPressedThisFrame) { Interact(); }
        }

        private void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("Player")) { interactable = true; } }
        private void OnTriggerExit2D(Collider2D other) { if (other.CompareTag("Player")) { interactable = false; } }

        protected abstract void Interact();
    }
}
