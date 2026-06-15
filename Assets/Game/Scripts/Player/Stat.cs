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
    public class Stat<T>
    {
        public event Action OnValueChange;
        
        [field: SerializeField]
        public T Value { get; set; }
        
        private List<Modifier<T>> _modifiers = new();
        
        public Stat() : this (default(T)) { }

        public Stat(T value)
        {
            Value = value;
        }

        public T GetValue()
        {
            var value = Value;
            if (_modifiers.Count <= 0) return value;
            foreach (var modifier in _modifiers)
                value = modifier.Value;
            return value;
        }
        public void AddModifier(Modifier<T> modifier)
        {
            _modifiers.Add(modifier);
            OnValueChange?.Invoke();
        }
        public void RemoveModifier(Modifier<T> modifier)
        {
            _modifiers.Remove(modifier);
            OnValueChange?.Invoke();
        }
        
        public void Refresh()
        {
            OnValueChange?.Invoke();
        }
    }
}