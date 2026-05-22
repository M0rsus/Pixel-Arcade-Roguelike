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

        public void Initialize()
        {
            ai.Initialize();
        }

        public void OnUpdate(float deltaTime)
        {
            ai.OnUpdate(deltaTime);
            ForwardInput = ai.IsMoving ? 1f : 0f;
            
            RotationInput = Mathf.Clamp(ai.Angle / 90f, -1f, 1f);
            if (Mathf.Abs(ai.Angle) < 1f)
                RotationInput = 0f;
            
            ShootInput = true;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            ai.OnFixedUpdate(deltaTime);
        }
    }
}