using System.Collections.Generic;
using Game;
using UnityEngine;

namespace UI
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField]
        private EntityInventory inventory;
        [SerializeField]
        private GameObject slotPrefab;
        [SerializeField] 
        private Transform container;
        
        private readonly List<InventorySlotView> _slots = new List<InventorySlotView>();

        private void Start()
        {
            if (inventory != null) inventory.OnInventoryChanged += RedrawInventory;
            RedrawInventory();
        }

        private void OnDestroy()
        {
            if (inventory != null) inventory.OnInventoryChanged -= RedrawInventory;
        }

        private void RedrawInventory()
        {
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }
            _slots.Clear();

            var currentSlots = inventory.Slots;
            foreach (var slot in currentSlots)
            {
                GameObject newSlot = Instantiate(slotPrefab, container);
                InventorySlotView uiSlot = newSlot.GetComponent<InventorySlotView>();
                
                uiSlot.DisplayItem(slot.card);
                _slots.Add(uiSlot);
            }
        }
    }
}