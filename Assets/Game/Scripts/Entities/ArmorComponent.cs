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

        private StatFloat _maxArmor;
        private StatFloat _armorRegen;
        private StatFloat _currentArmor;

        public void Initialize(Stats stats)
        {
            _maxArmor = stats.maxArmor;
            _currentArmor = new(_maxArmor.GetValue());
            _armorRegen = stats.armorRegen;
            if (armorView)
                armorView.Initialize(_currentArmor, _maxArmor);
            
            _cts = AsyncLifecycleManager.CreateLinkedSource();
            _ct = _cts.Token;
            
            _maxArmor.OnChanged += Recalculate;
        }

        private void Recalculate(float oldArmor, float newArmor)
        {
            Heal(newArmor - oldArmor);
        }

        public bool HasArmor()
        {
            return _maxArmor.GetValue() > 0 && _armorRegen.GetValue() > 0;
        }

        public float TakeDamage(int damage)
        {
            float maxArmor = _maxArmor.GetValue();
            float currentArmor = _currentArmor.Value;
            currentArmor -= damage;
            float excessDamage = -currentArmor;
            
            if (currentArmor >= maxArmor) OnArmorFull?.Invoke();
            if (currentArmor <= 0) OnArmorLost?.Invoke();
            
            _currentArmor.Value = Mathf.Clamp(currentArmor, 0, maxArmor);
            
            return excessDamage;
        }
        
        public void Heal(float amount) 
        {
            float maxArmor = _maxArmor.GetValue();
            float currentArmor = _currentArmor.GetValue();
            
            currentArmor += amount;
            
            if (currentArmor >= maxArmor) OnArmorFull?.Invoke();
            if (currentArmor <= 0) OnArmorLost?.Invoke();
            
            _currentArmor.Value = Mathf.Clamp(currentArmor, 0, maxArmor);
        }

        private async UniTaskVoid Regen()
        {
            try
            {
                await UniTask.Delay(
                    TimeSpan.FromSeconds(cooldownArmorRegen),
                    ignoreTimeScale: false,
                    cancellationToken: _ct);

                while (_currentArmor.Value < _maxArmor.GetValue())
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

        public void ActivateRegen()
        {
            if (_currentArmor.Value >= _maxArmor.GetValue()) return;
            CancelRegen();
            Regen().Forget();
        }
        
        private void ClearCancellationTokenSource()
        {
            if (_cts == null) return;
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }

        public void CancelRegen()
        {
            ClearCancellationTokenSource();
            _cts = AsyncLifecycleManager.CreateLinkedSource();
            _ct = _cts.Token;
        }

        public void OnDestroy()
        {
            ClearCancellationTokenSource();
            _maxArmor.OnChanged -= Recalculate;
        }
    }
}