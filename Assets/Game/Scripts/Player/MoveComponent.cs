using System;
using NaughtyAttributes;
using UnityEngine;

namespace Game
{
    [Serializable]
    public sealed class MoveComponent
    {
        [SerializeField]
        private float movementSpeed;
        [SerializeField]
        private float rotationSpeed;
        [SerializeField] 
        private Rigidbody2D rigidbody;

        private IControllable _controllable;
        
        public void Initialize(IControllable controllable)
        {
            _controllable = controllable;
        }
        
        public void OnFixedUpdate(float deltaTime)
        {
            var rotation = _controllable.RotationInput * rotationSpeed * deltaTime;
            rigidbody.MoveRotation(rigidbody.rotation + rotation);

            Vector2 move = rigidbody.transform.up * _controllable.ForwardInput * movementSpeed * deltaTime;
            rigidbody.AddForce(move);
        }
    }
}

