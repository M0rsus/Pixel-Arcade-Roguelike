using System.Collections.Generic;
using Cards;
using Game;
using UnityEngine;

namespace UI
{
    public class CardSpawnManager : MonoBehaviour
    {
        private readonly List<GameObject> _cardSpawners = new();
        [SerializeField] 
        private EntityRarities playerRarities;

        private void Awake()
        {
            foreach(Transform child in transform)
                _cardSpawners.Add(child.gameObject);
        }

        public void SpawnCard()
        {
            foreach (GameObject cardSpawner in _cardSpawners)
            {
                List<CardView> cards = CardRegistry.Cards;
                Rarity currentRarity = SelectRarity();
                Debug.Log($"Current rarity is {currentRarity}");
                
                if (currentRarity == null)
                {
                    SpawnEmptyCard();
                    continue;
                }
                
                List<CardView> selectedCards = cards.FindAll(c => currentRarity.Name == c.Card.rarity.Name);
                
                if (selectedCards.Count <= 0)
                {
                    SpawnEmptyCard();
                    continue;
                }
                Instantiate(selectedCards[Random.Range(0, selectedCards.Count)], cardSpawner.transform);
            }
        }

        private Rarity SelectRarity()
        {
            int randomWeight = Random.Range(0, playerRarities.GetTotalWeight());
            int currentWeight = 0;
            foreach (Rarity rarity in playerRarities.Rarities)
            {
                currentWeight += rarity.Weight;
                if (randomWeight <= currentWeight)
                    return rarity;
            }
            return null;
        }
        
        private void SpawnEmptyCard()
        {
            
        }
        public void DeleteAllCards()
        {
            foreach (GameObject cardSpawner in _cardSpawners)
                foreach (Transform child in cardSpawner.transform)
                    Destroy(child.gameObject);
        }
    }
}