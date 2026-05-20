using Demo;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class EnemyInput : IMoveable, IRotatable, IShootable
    {
        [SerializeReference] [SRDemo]
        private AI ai;
        public float ForwardInput { get; private set; }
        public float RotationInput {get; private set;}
        public bool ShootInput { get; private set; }

        public void OnUpdate(float deltaTime)
        {
            ai.OnUpdate(deltaTime);
            ForwardInput = ai.IsMoving ? 1f : 0f;
            
            switch (ai.Angle)
            { 
                case > 2f:
                    RotationInput = 1f;
                    break;
                case < -2f:
                    RotationInput = -1f;
                    break;
                default:
                    RotationInput = 0f;
                    break;
            }
            ShootInput = true;
        }
    }
}