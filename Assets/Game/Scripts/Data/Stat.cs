using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class StatFloat : Stat<float>
    {
        public StatFloat() : base() { }
        public StatFloat(float value) : base(value) { }
        
        public override float GetValue()
        {
            var localValue = Value;
            if (_modifiers.Count <= 0) return localValue;
            foreach (var modifier in _modifiers)
            {
                switch (modifier.Type)
                {
                    case Modifier<float>.ModifierType.Add:
                        localValue += modifier.Value;
                        break;
                    case Modifier<float>.ModifierType.Subtract:
                        localValue -= modifier.Value;
                        break;
                    case Modifier<float>.ModifierType.Multiply:
                        localValue *= modifier.Value;
                        break;
                    case Modifier<float>.ModifierType.Divide:
                        localValue /= modifier.Value;
                        break;
                }
            }
            return localValue;
        }
        protected override void Refresh(float oldValue, float newValue)
        {
            Debug.Log($"{nameof(StatFloat)}: {GetValue()}, oldValue: {oldValue}, newValue: {newValue}");
            ValueChanged(oldValue, GetValue());
        }
    }

    [Serializable]
    public class StatInt : Stat<int>
    {
        public StatInt() : base() { }
        public StatInt(int value) : base(value) { }
        
        public override int GetValue()
        {
            var localValue = Value;
            if (_modifiers.Count <= 0) return localValue;
            foreach (var modifier in _modifiers)
            {
                switch (modifier.Type)
                {
                    case Modifier<int>.ModifierType.Add:
                        localValue += modifier.Value;
                        break;
                    case Modifier<int>.ModifierType.Subtract:
                        localValue -= modifier.Value;
                        break;
                    case Modifier<int>.ModifierType.Multiply:
                        localValue *= modifier.Value;
                        break;
                    case Modifier<int>.ModifierType.Divide:
                        localValue /= modifier.Value;
                        break;
                }
            }
            return localValue;
        }
        protected override void Refresh(int oldValue, int newValue)
        {
            Debug.Log($"{nameof(StatInt)}: {GetValue()}, oldValue: {oldValue}, newValue: {newValue}");
            ValueChanged(oldValue, GetValue());
        }
    }

    [Serializable]
    public class StatBool : Stat<bool>
    {
        public StatBool() : base() { }
        public StatBool(bool value) : base(value) { }
        public override bool GetValue()
        {
            var localValue = Value;
            if (_modifiers.Count <= 0) return localValue;
            foreach (var modifier in _modifiers)
                localValue = modifier.Value;
            return localValue;
        }
    }
    
    [Serializable]
    public class Stat<T> where T : struct, IEquatable<T>
    {
        [SerializeField]
        private T value;
        private PrimitiveComparer<T> _comparer;
        public event Action<T, T> OnChanged;
        public event Action OnUpdated;
        
        protected List<Modifier<T>> _modifiers = new();

        public T Value
        {
            get => value;
            set
            {
                if (_comparer.Equals(this.value, value)) return;
                T oldValue = value;
                this.value = value;
                ValueChanged(oldValue, value);
                OnUpdated?.Invoke();
                //Debug.Log($"Changing {oldValue} to {value}");
            }
        }

        public Stat()
        {
            value = default;
        }

        public Stat(T value)
        {
            this.value = value;
        }

        protected void ValueChanged(T oldValue, T newValue)
        {
            OnChanged?.Invoke(oldValue, newValue);
        }

        public virtual T GetValue()
        {
            var localValue = Value;
            if (_modifiers.Count <= 0) return localValue;
            foreach (var modifier in _modifiers)
                localValue = modifier.Value;
            return localValue;
        }
        public void AddModifier(Modifier<T> modifier)
        {
            T oldValue = GetValue();
            _modifiers.Add(modifier);
            modifier.OnChanged += Refresh;
            modifier.OnUpdated += Refresh;
            ValueChanged(oldValue, GetValue());
            OnUpdated?.Invoke();
        }
        public void RemoveModifier(Modifier<T> modifier)
        {
            T oldValue = GetValue();
            _modifiers.Remove(modifier);
            modifier.OnChanged -= Refresh;
            modifier.OnUpdated -= Refresh;
            ValueChanged(oldValue, GetValue());
            OnUpdated?.Invoke();
        }

        private void Refresh()
        {
            OnUpdated?.Invoke();
        }
        protected virtual void Refresh(T oldValue, T newValue) { }
    }
}