using System;
using Boxfriend.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Boxfriend.Player
{
    public class PlayerInputProvider : MonoBehaviour, IClientSideComponent
    {
        private InputActions _inputActions;
        private InputActions.PlayerActions _playerActions;

        public event Action<Vector3> OnMovement;
        public event Action<float> OnRotation;
        public event Action OnInteract, OnToggleLight;
        public event Action<bool> OnSprint;

        public void ActivateClient ()
        {
            _inputActions = new();
            _playerActions = _inputActions.Player;

            _playerActions.Movement.performed += OnMove; 
            _playerActions.Movement.canceled += OnMove;

            _playerActions.Look.performed += ctx => OnRotation?.Invoke(ctx.ReadValue<float>());
            _playerActions.Look.canceled += ctx => OnRotation?.Invoke(ctx.ReadValue<float>());
            
            _playerActions.Interact.performed += _ => OnInteract?.Invoke();
            _playerActions.ToggleLight.performed += _ => OnToggleLight?.Invoke();

            _playerActions.Sprint.performed += _ => OnSprint?.Invoke(true);
            _playerActions.Sprint.canceled += _ => OnSprint?.Invoke(false);

            OnEnable();
        }

        private void OnEnable ()
        {
            if (_inputActions is null)
                return;

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
