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
            if (ai.IsMoving)
                ForwardInput = 1f;
            else
                ForwardInput = 0f;
            
            switch (ai.Angle)
            { 
                case > 1:
                    RotationInput = 1f;
                    break;
                case < -1:
                    RotationInput = -1f;
                    break;
                default:
                    RotationInput = 0f;
                    break;
            }
            Debug.Log(ai.Angle);
            ShootInput = true;
        }
    }
}