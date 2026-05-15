using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Stat
    {
        [field: SerializeField]
        public float Value { get; private set; }
        
        private List<Modifier> _modifiers = new List<Modifier>();

        public float GetValue()
        {
            float value = Value;
            if (_modifiers.Count > 0) {}
            foreach (var modifier in _modifiers)
                value = modifier.Value;
            return value;
        }
        public void AddModifier(Modifier modifier)
        {
            _modifiers.Add(modifier);
        }
        public void RemoveModifier(Modifier modifier)
        {
            _modifiers.Remove(modifier);
        }
    }
}