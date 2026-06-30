using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public sealed class PlayerInput : MonoBehaviour, IMoveable, IRotatable, IShootable
    {
        public float ForwardInput { get; private set; }
        public float RotationInput {get; private set;}
        public event System.Action<bool> OnShoot;
        
        public void OnForward(InputAction.CallbackContext context)
        {
            ForwardInput = context.ReadValue<float>();
        }

        public void OnRotation(InputAction.CallbackContext context)
        {
            RotationInput = context.ReadValue<float>();
        }

        public void ShootInput(InputAction.CallbackContext context)
        {
            if (context.started)
                OnShoot?.Invoke(true);
            if (context.canceled)
                OnShoot?.Invoke(false);
        }
    }
}