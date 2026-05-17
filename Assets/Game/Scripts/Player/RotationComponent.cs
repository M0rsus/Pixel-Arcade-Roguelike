using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class RotationComponent
    {
        private Rigidbody2D _rigidbody;
        private IRotatable _rotatable;
        private Stat _rotationSpeed;
        
        public void Initialize(Rigidbody2D rb, IRotatable rotatable, Stats stats)
        {
            _rigidbody = rb;
            _rotatable = rotatable;
            _rotationSpeed = stats.rotationSpeed;
        }
        
        public void OnFixedUpdate(float deltaTime)
        {
            var rotation = _rotatable.RotationInput * _rotationSpeed.GetValue() * deltaTime;
            _rigidbody.MoveRotation(_rigidbody.rotation + rotation);
        }
    }
}