using System;
using Cards;
using Game;
using UnityEngine;

namespace UI
{
    public class TempButton : MonoBehaviour
    {
        [SerializeField] private GameObject spawn;
        [SerializeField] private GameObject cardSpawn;
        [SerializeField] private EntityInventory inventory;
        public static event Action OnCardCreate;

        public void CreateCard()
        {
            Instantiate(CardRegistry.Cards[0], cardSpawn.transform);
            OnCardCreate?.Invoke();
        }

        public void DeleteCard()
        {
            inventory.RemoveCard(inventory.Slots.Count - 1);
        }
    }
}