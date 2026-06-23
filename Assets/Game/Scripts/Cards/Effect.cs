using Game;
using UnityEngine;

namespace Cards
{
    public class Effect
    {
        protected Stats _stats;
        protected GameObject _entity;

        public virtual void Initialize(Stats stats, GameObject entity)
        {
            _stats = stats;
            _entity = entity;
        }

        public virtual void Destroy() { }
    }
}