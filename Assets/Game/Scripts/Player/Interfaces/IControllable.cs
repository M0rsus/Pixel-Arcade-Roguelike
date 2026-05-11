using UnityEngine;

namespace Game
{
    public interface IControllable
    {
        public float ForwardInput { get; }
        public float RotationInput { get; }
        public bool ShootInput { get; }
    }
}