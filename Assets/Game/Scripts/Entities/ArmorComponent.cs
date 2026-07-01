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
        private UI.SliderView armorView;
        [SerializeField] 
        private float cooldownArmorRegen = 4f;
        [SerializeField]
        private float delayRegen = 1f;

        public StatFloat MaxArmor { get; private set; }
        private StatFloat _armorRegen;
        private StatFloat _currentArmor;
        
        public bool IsActiveRegen { get; set; }

        public void Initialize(Stats stats)
        {
            MaxArmor = stats.maxArmor;
            _currentArmor = new(MaxArmor.GetValue());
            _armorRegen = stats.armorRegen;
            if (armorView)
                armorView.Initialize(_currentArmor, MaxArmor);
            _cts = AsyncLifecycleManager.CreateLinkedSource();
            _ct = _cts.Token;
            MaxArmor.OnChanged += Recalculate;
            MaxArmor.OnUpdated += ShouldRegenerate;
            _currentArmor.OnUpdated += ShouldRegenerate;
            _armorRegen.OnUpdated += ShouldRegenerate;
        }

        private void Recalculate(float oldArmor, float newArmor)
        {
            Heal(newArmor - oldArmor);
        }

        public bool CheckArmor()
        {
            return MaxArmor.GetValue() > 0 && _armorRegen.GetValue() > 0;
        }

        public float TakeDamage(int damage)
        {
            float maxArmor = MaxArmor.GetValue();
            float currentArmor = _currentArmor.GetValue();
            
            float prevArmor = _currentArmor.Value;
            currentArmor -= damage;
            
            float excessDamage = -currentArmor;
            
            if (currentArmor <= 0 && prevArmor > 0) OnArmorLost?.Invoke();
            if (currentArmor >= maxArmor) OnArmorFull?.Invoke();
            
            _currentArmor.Value = Mathf.Clamp(currentArmor, 0, maxArmor);
            
            ClearCancellationTokenSource();
            _cts = AsyncLifecycleManager.CreateLinkedSource();
            _ct = _cts.Token;
            
            return excessDamage;
        }
        
        public void Heal(float amount) 
        {
            float maxArmor = MaxArmor.GetValue();
            float currentArmor = _currentArmor.GetValue();
            
            currentArmor += amount;
            
            if (currentArmor >= maxArmor) OnArmorFull?.Invoke();
            if (currentArmor <= 0) OnArmorLost?.Invoke();
            
            _currentArmor.Value = Mathf.Clamp(currentArmor, 0, maxArmor);
        }

        public async UniTaskVoid Regen()
        {
            try
            {
                await UniTask.Delay(
                    TimeSpan.FromSeconds(cooldownArmorRegen), 
                    ignoreTimeScale: false, 
                    cancellationToken: _ct);
                
                while (_currentArmor.Value < MaxArmor.GetValue())
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(delayRegen),
                        ignoreTimeScale: false,
                        cancellationToken: _ct);
                
                    float regenArmor = _armorRegen.GetValue() * delayRegen;
                    Heal(regenArmor);
                }
            }
            catch (OperationCanceledException) { }
        }
        private void ShouldRegenerate()
        {
            if (_currentArmor.Value < MaxArmor.GetValue() && IsActiveRegen)
                Regen().Forget();
        }
        public void ClearCancellationTokenSource()
        {
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }

        public void OnDestroy()
        {
            ClearCancellationTokenSource();
            MaxArmor.OnChanged -= Recalculate;
            MaxArmor.OnUpdated -= ShouldRegenerate;
            _currentArmor.OnUpdated -= ShouldRegenerate;
            _armorRegen.OnUpdated -= ShouldRegenerate;
        }
    }
}