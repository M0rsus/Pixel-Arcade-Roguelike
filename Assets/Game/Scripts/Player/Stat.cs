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
    }

    [Serializable]
    public class StatInt : Stat<int>
    {
        public StatInt() : base() { }
        public StatInt(int value) : base(value) { }
    }

    [Serializable]
    public class StatBool : Stat<bool>
    {
        public StatBool() : base() { }
        public StatBool(bool value) : base(value) { }
    }
    
    [Serializable]
    public class Stat<T> where T : struct, IEquatable<T>
    {
        [SerializeField]
        private T value;
        private PrimitiveComparer<T> _comparer;
        public event Action<T, T> OnChanged;
        public event Action OnUpdated;
        
        private List<Modifier<T>> _modifiers = new();

        public T Value
        {
            get => value;
            set
            {
                if (_comparer.Equals(this.value, value)) return;
                T oldValue = value;
                this.value = value;
                OnChanged?.Invoke(oldValue, value);
                OnUpdated?.Invoke();
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

        public T GetValue()
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
            OnChanged?.Invoke(oldValue, GetValue());
            OnUpdated?.Invoke();
        }
        public void RemoveModifier(Modifier<T> modifier)
        {
            T oldValue = GetValue();
            _modifiers.Remove(modifier);
            OnChanged?.Invoke(oldValue, GetValue());
            OnUpdated?.Invoke();
        }
        
        public void Refresh()
        {
            OnUpdated?.Invoke();
        }
    }
}