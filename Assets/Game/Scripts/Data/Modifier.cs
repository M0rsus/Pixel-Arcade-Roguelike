using System;
using UnityEngine;

namespace Game
{
    public class Modifier<T> where T : struct, IEquatable<T>
    {
        public event Action OnUpdated;
        public event Action<T, T> OnChanged;
        private T _value;
        private PrimitiveComparer<T> _comparer;

        public T Value
        {
            get => _value;
            set
            {
                if (_comparer.Equals(_value, value)) return;
                T oldValue = _value;
                _value = value;
                OnChanged?.Invoke(oldValue, value);
                OnUpdated?.Invoke();
            }
        }

        public ModifierType Type { get; private set; }

        public Modifier(T value, ModifierType type)
        {
            Value = value;
            Type = type;
        }
        public enum ModifierType
        {
            Add,
            Subtract,
            Multiply,
            Divide,
        }
    }
}