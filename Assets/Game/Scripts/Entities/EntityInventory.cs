using System;
using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace Game
{
    public class EntityInventory : MonoBehaviour
    {
        public event Action OnInventoryChanged;
        [SerializeField]
        private List<InventorySlot> slots = new List<InventorySlot>();

        public void AddCard(Card card)
        {
            slots.Add(new InventorySlot(card));
            OnInventoryChanged?.Invoke();
        }

        public void RemoveCard(int index)
        {
            if (index < 0 || index >= slots.Count) return;
            slots.RemoveAt(index);
            OnInventoryChanged?.Invoke();
        }
    }
}