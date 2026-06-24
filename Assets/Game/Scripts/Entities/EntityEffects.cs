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

        public void AddEffect(BaseCard card)
        {
            Effect effect = card.GetEffect();
            
            if (effect == null) return;
            effect.Initialize(stats, entity);
            _effects.Add(effect);
        }

        public void RemoveEffect(BaseCard card)
        {
            Effect effect = card.GetEffect();
            
            if (effect == null) return;
            effect.Destroy();
            _effects.Remove(effect);
        }
    }
}