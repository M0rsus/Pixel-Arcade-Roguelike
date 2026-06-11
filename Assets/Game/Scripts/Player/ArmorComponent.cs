using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class ArmorComponent
    {
        public event Action OnArmorFull;
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
            int maxArmor = MaxArmor.GetValue();
            
            float prevArmor = _currentArmor;
            _currentArmor -= damage;
            
            float excessDamage = -_currentArmor;
            
            if (_currentArmor <= 0 && prevArmor > 0) OnArmorLost?.Invoke();
            if (_currentArmor >= maxArmor) OnArmorFull?.Invoke();
            
            _currentArmor = Mathf.Clamp(_currentArmor, 0, maxArmor);
            
            ClearCancellationTokenSource();
            _cts = CancellationTokenSource.CreateLinkedTokenSource(_ct);
            Regen().Forget();
            
            return excessDamage;
        }
        
        public void Heal(float amount) 
        {
            int maxArmor = MaxArmor.GetValue();
            
            _currentArmor += amount;
            
            if (_currentArmor >= maxArmor) OnArmorFull?.Invoke();
            if (_currentArmor <= 0) OnArmorLost?.Invoke();
            
            _currentArmor = Mathf.Clamp(_currentArmor, 0, maxArmor);
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
                
                while (_currentArmor < MaxArmor.GetValue())
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
        public void ClearCancellationTokenSource()
        {
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}