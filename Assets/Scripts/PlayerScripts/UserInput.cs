using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerScripts
{
    public class UserInput : MonoBehaviour
    {
        private PlayerControls _controls;

        private void Awake() => _controls = new PlayerControls();

        public void OnEnable() => _controls.Enable();

        public void OnDisable() => _controls.Disable();

        public Vector2 Movement { get; private set; }
      
        // The below commented code is the same as the above,
        // but shows the processes behind-the-scenes.
      
        /*{
          get => movement;
          private set => movement = value;
      } */
        
        public static bool Interact;
        public static bool Pause;
        
        public void Update()
        {
            Movement = _controls.Player.Movement.ReadValue<Vector2>();
            Interact = _controls.Interact.Keyboard.triggered;
            Pause = _controls.Pause.OpenMenu.triggered;
        }
    }
}