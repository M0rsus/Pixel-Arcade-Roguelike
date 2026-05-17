using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public sealed class PlayerInput : MonoBehaviour, IMoveable, IRotatable, IShootable
    {
        public float ForwardInput { get; private set; }
        public float RotationInput {get; private set;}
        public bool ShootInput { get; private set; }
        
        public void OnForward(InputAction.CallbackContext context)
        {
            ForwardInput = context.ReadValue<float>();
        }

        public void OnRotation(InputAction.CallbackContext context)
        {
            RotationInput = context.ReadValue<float>();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            ShootInput = context.performed;
        }
    }
}