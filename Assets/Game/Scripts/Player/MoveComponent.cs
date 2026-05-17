using System;
using NaughtyAttributes;
using UnityEngine;

namespace Game
{
    [Serializable]
    public sealed class MoveComponent
    {
        [SerializeField]
        private Mode mode;
        private Rigidbody2D _rigidbody;
        private IMoveable _moveable;
        private Stat _moveSpeed;
        
        public void Initialize(Rigidbody2D rb, IMoveable moveable, Stats stats)
        {
            _rigidbody = rb;
            _moveable = moveable;
            _moveSpeed = stats.moveSpeed;
        }
        
        public void OnFixedUpdate(float deltaTime)
        {
            Vector2 move = _rigidbody.transform.up * _moveable.ForwardInput * _moveSpeed.GetValue() * deltaTime;
            switch (mode)
            {
                case Mode.AddForce:
                    _rigidbody.AddForce(move);
                    break;
                case Mode.MovePosition:
                    _rigidbody.MovePosition(_rigidbody.position + move);
                    break;
            }
        }

        private enum Mode
        {
            AddForce,
            MovePosition
        }
    }
}

