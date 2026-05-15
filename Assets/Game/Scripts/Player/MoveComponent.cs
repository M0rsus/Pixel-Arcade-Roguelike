using System;
using NaughtyAttributes;
using UnityEngine;

namespace Game
{
    [Serializable]
    public sealed class MoveComponent
    {
        private Stat _moveSpeed;
        private Stat _rotationSpeed;
        [SerializeField] 
        private Rigidbody2D rigidbody;

        private IControllable _controllable;
        
        public void Initialize(Stats stats, IControllable controllable)
        {
            _moveSpeed = stats.moveSpeed;
            _rotationSpeed = stats.rotationSpeed;
            _controllable = controllable;
        }
        
        public void OnFixedUpdate(float deltaTime)
        {
            var rotation = _controllable.RotationInput * _rotationSpeed.GetValue() * deltaTime;
            rigidbody.MoveRotation(rigidbody.rotation + rotation);

            Vector2 move = rigidbody.transform.up * _controllable.ForwardInput * _moveSpeed.GetValue() * deltaTime;
            rigidbody.AddForce(move);
        }
    }
}

