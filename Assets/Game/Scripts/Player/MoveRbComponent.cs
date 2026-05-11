using System;
using NaughtyAttributes;
using UnityEngine;

namespace Game
{
    [Serializable]
    public sealed class MoveRbComponent
    {
        [SerializeField]
        private float movementSpeed;
        [SerializeField]
        private float rotationSpeed;
        [SerializeField] 
        private Rigidbody2D rb;

        private IControllable _controllable;
        
        public void Initialize(IControllable controllable)
        {
            _controllable = controllable;
        }
        
        public void OnFixedUpdate(float fixedDeltaTime)
        {
            var rotation = _controllable.RotationInput * rotationSpeed * fixedDeltaTime;
            rb.MoveRotation(rb.rotation + rotation);

            Vector2 move = rb.transform.up * _controllable.ForwardInput * movementSpeed * fixedDeltaTime;
            rb.AddForce(move);
        }
    }
}

