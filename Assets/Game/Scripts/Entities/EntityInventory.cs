using System;
using System.Collections.Generic;
using Cards;
using UI;
using UnityEngine;

namespace Game
{
    public class EntityInventory : MonoBehaviour
    {
        public event Action OnInventoryChanged;
        [SerializeField]
        private List<InventorySlot> slots = new List<InventorySlot>();
        [SerializeField]
        private EntityEffects effects;

        private void AddCard(Card card)
        {
            slots.Add(new InventorySlot(card));
            effects.AddEffect(card);
            OnInventoryChanged?.Invoke();
        }

        public void RemoveCard(int index)
        {
            if (index < 0 || index >= slots.Count) return;
            slots.RemoveAt(index);
            effects.RemoveEffect(index);
            OnInventoryChanged?.Invoke();
        }

        private void OnEnable()
        {
            CardView.OnPickedCard += AddCard;
        }

        private void OnDisable()
        {
            CardView.OnPickedCard -= AddCard;
        }
    }
}