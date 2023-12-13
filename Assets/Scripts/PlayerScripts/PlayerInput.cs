using UnityEngine;

namespace PlayerScripts
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerControls _controls;

        private void Awake() => _controls = new PlayerControls();

        private void OnEnable() => _controls.Enable();

        private void OnDisable() => _controls.Disable();

        public Vector2 Movement { get; private set; }
      
        // The below commented code is the same as the above,
        // but shows the processes behind-the-scenes.
      
        /*{
          get => movement;
          private set => movement = value;
      } */
    
        private void Update()
        {
            Movement = _controls.Player.Movement.ReadValue<Vector2>();
        }
    }
}