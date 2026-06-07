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
            ForwardInput = ai.GetState() switch
            {
                "Forward" => 1f,
                "Idle" => 0f,
                "Backward" => -1f,
                _ => ForwardInput
            };
            
            RotationInput = Mathf.Clamp(ai.Angle / 90f, -1f, 1f);
            if (Mathf.Abs(ai.Angle) < 1f)
                RotationInput = 0f;
            
            ShootInput = true;
            //Debug.Log($"ForwardInput: <color=yellow>{ForwardInput}</color>");
            //Debug.Log($"RotationInput: <color=yellow>{RotationInput}</color>");
        }

        public void OnFixedUpdate(float deltaTime)
        {
            ai.OnFixedUpdate(deltaTime);
        }
    }
}