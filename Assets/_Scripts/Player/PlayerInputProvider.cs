using System;
using Boxfriend.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Boxfriend.Player
{
    public class PlayerInputProvider : MonoBehaviour
    {
        private InputActions _inputActions;
        private InputActions.PlayerActions _playerActions;

        public event Action<Vector3> OnMovement;
        public event Action<float> OnRotation;
        public event Action OnInteract, OnToggleLight;

        private void Awake ()
        {
            _inputActions = new();
            _playerActions = _inputActions.Player;

            _playerActions.Movement.performed += OnMove; 
            _playerActions.Movement.canceled += OnMove;

            _playerActions.Look.performed += ctx => OnRotation?.Invoke(ctx.ReadValue<float>());
            _playerActions.Look.canceled += ctx => OnRotation?.Invoke(ctx.ReadValue<float>());
            
            _playerActions.Interact.performed += _ => OnInteract?.Invoke();
            _playerActions.ToggleLight.performed += _ => OnToggleLight?.Invoke();
        }

        private void OnEnable ()
        {
            _playerActions.Enable();
        }
        private void OnDisable ()
        {
            _playerActions.Disable();
        }

        private void OnMove(InputAction.CallbackContext ctx)
        {
            var dir = ctx.ReadValue<Vector2>();
            var input = new Vector3(dir.x, 0, dir.y);
            OnMovement?.Invoke(input);
        }
    }
}
