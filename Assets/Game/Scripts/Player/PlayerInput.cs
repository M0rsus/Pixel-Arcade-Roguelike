using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public sealed class PlayerInput : MonoBehaviour, IControllable
    {
        public float ForwardInput { get; private set; }
        public float RotationInput {get; private set;}
        public bool ShootInput { get; private set; }
        
        private void OnForward(InputValue value)
        {
            Debug.Log("ForwardInput: " + ForwardInput);
            ForwardInput = value.Get<float>();
            
        }

        private void OnRotation(InputValue value)
        {
            Debug.Log("RotationInput: " + RotationInput);
            RotationInput = value.Get<float>();
            
        }

        private void OnShoot(InputValue value)
        {
            ShootInput = value.isPressed;
            Debug.Log("ShootInput: " + ShootInput);
        }
    }
}