using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerScripts
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerControls _controls;

        private void Awake() => _controls = new PlayerControls();

        internal void OnEnable() => _controls.Enable();

        internal void OnDisable() => _controls.Disable();

        public Vector2 Movement { get; private set; }
      
        // The below commented code is the same as the above,
        // but shows the processes behind-the-scenes.
      
        /*{
          get => movement;
          private set => movement = value;
      } */
        
        public bool interact;
        public bool pause;
    
        public void Update()
        {
            Movement = _controls.Player.Movement.ReadValue<Vector2>();
            interact = _controls.Interact.Keyboard.triggered;
            pause = _controls.Pause.OpenMenu.triggered;
        }
    }
}