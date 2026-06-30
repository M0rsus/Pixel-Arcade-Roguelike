using System.Threading;
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
        public event System.Action<bool> OnShoot;

        public void Initialize(CancellationToken ct, CancellationToken stuckToken)
        {
            ai.Initialize(ct, stuckToken);
        }

        public void OnUpdate(float deltaTime)
        {
            ai.OnUpdate(deltaTime);
            ForwardInput = ai.GetState() switch
            {
                "Forward" => 1f,
                "Idle" => 0f,
                "Backward" => -0.5f,
                _ => ForwardInput
            };
            
            RotationInput = Mathf.Clamp(ai.Angle / 90f, -1f, 1f);
            if (Mathf.Abs(ai.Angle) < 1f)
                RotationInput = 0f;
        }

        public void OnDestroy()
        {
            ai.ClearCancellationTokenSource();
            ai.OnDestroy();
        }

        public void OnFixedUpdate(float deltaTime)
        {
            ai.OnFixedUpdate(deltaTime);
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
            ai.OnCollisionStay2D(collision);
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            ai.OnCollisionExit2D(collision);
        }
    }
}