using UnityEngine;

namespace PlayerScripts
{
    public class UserInput : MonoBehaviour
    {
        private PlayerControls _controls;

        private void Awake() => _controls = new PlayerControls();

        // Use this after cutscenes!
        public void OnEnable()
        {
            _controls.Enable(); 
        }

        // Use this during cutscenes!
        public void OnDisable()
        { 
            _controls.Disable();
        } 

        public void SwitchInputToPauseScreen()
        {
            _controls.Player.Disable();
            _controls.TitleScreen.Disable();
            _controls.PauseScreen.Enable();
        }
        public void SwitchInputToGameScene()
        {
            _controls.PauseScreen.Disable();
            _controls.TitleScreen.Disable();
            _controls.Player.Enable();
        }
        public void SwitchInputToTitleScreen()
        {
            _controls.PauseScreen.Disable();
            _controls.Player.Disable(); 
            _controls.TitleScreen.Enable();
        }

        // The above commented code is the same as the below for Movement,
        // but shows the processes behind-the-scenes.
        
        /* { get => movement;
          private set => movement = value; } */
        
        // Player ActionMap Controls:
        public Vector2 Movement { get; private set; }
        public static bool Interact { get; private set; }
        public static bool Pause { get; private set; }
        
        // Title Screen ActionMap Controls:
        public static bool AnyKeyOrStart { get; private set; }
        
        // PauseScreen ActionMap Controls:
        public Vector2 Move { get; private set; }
        public static bool Unpause { get; private set; }
        public static bool Select { get; private set; }
        
        public void Update()
        {
            Movement = _controls.Player.Movement.ReadValue<Vector2>();
            Interact = _controls.Player.Interact.triggered;
            Pause = _controls.Player.OpenPauseMenu.triggered;

            AnyKeyOrStart = _controls.TitleScreen.AnyKeyorStart.triggered;

            Move = _controls.PauseScreen.Move.ReadValue<Vector2>();
            Unpause = _controls.PauseScreen.Unpause.triggered;
            Select = _controls.PauseScreen.Select.triggered;
        }
    }
}