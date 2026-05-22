using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class ArmorComponent
    {
        public event Action OnArmorLost;
        
        private CancellationToken _ct;
        private CancellationTokenSource _cts;
        
        [SerializeField] 
        private float cooldownArmorRegen = 4f;
        [SerializeField]
        private float delayRegen = 1f;
        
        public StatInt MaxArmor { get; private set; }
        private StatFloat _armorRegen;
        [SerializeField]
        private float _currentArmor;
        
        public bool IsActiveRegen { get; set; }

        public void Initialize(Stats stats, CancellationToken ct)
        {
            MaxArmor = stats.maxArmor;
            _currentArmor = MaxArmor.GetValue();
            _armorRegen = stats.armorRegen;
            _ct = ct;
            _cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        }

        public bool CheckArmor()
        {
            return MaxArmor.GetValue() > 0 && _armorRegen.GetValue() > 0;
        }

        public float TakeDamage(float damage)
        {
            float prevArmor = _currentArmor;
            _currentArmor -= damage;
            float excessDamage = -_currentArmor;
            if (_currentArmor <= 0)
            {
                if (prevArmor > 0)
                    OnArmorLost?.Invoke();
                _currentArmor = 0;
            }
            CancelRegen();
            Regen().Forget();
            return excessDamage;
        }
        
        public void Heal(float amount)
        {
            _currentArmor += amount;
            if (_currentArmor > MaxArmor.GetValue())
                _currentArmor = MaxArmor.GetValue();
            
            if (_currentArmor > 0) return;
            _currentArmor = 0;
            OnArmorLost?.Invoke();
        }

        public async UniTaskVoid Regen()
        {
            try
            {
                if (!IsActiveRegen) return;
                
                await UniTask.Delay(
                    TimeSpan.FromSeconds(cooldownArmorRegen), 
                    ignoreTimeScale: false, 
                    cancellationToken: _cts.Token);
                
                while (_currentArmor < MaxArmor.GetValue() && _currentArmor >= 0)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(delayRegen),
                        ignoreTimeScale: false,
                        cancellationToken: _cts.Token);
                
                    float regenArmor = _armorRegen.GetValue() * delayRegen;
                    Heal(regenArmor);
                }
            }
            catch (OperationCanceledException) { }
        }
        public void CancelRegen()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
            
            _cts = CancellationTokenSource.CreateLinkedTokenSource(_ct);
        }
    }
}