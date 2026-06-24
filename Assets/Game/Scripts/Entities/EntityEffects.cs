using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace Game
{
    public class EntityEffects : MonoBehaviour
    {
        [SerializeField] 
        private Stats stats;
        [SerializeField]
        private GameObject entity;
        
        private readonly List<Effect> _effects = new List<Effect>();

        public void AddEffect(Card card)
        {
            Effect effect = card.GetEffect();
            
            if (effect == null) return;
            effect.Initialize(stats, entity);
            _effects.Add(effect);
        }

        public void RemoveEffect(int index)
        {
            if (index < 0 || index >= _effects.Count) return;
            _effects[index].Destroy();
            _effects.RemoveAt(index);
        }
    }
}