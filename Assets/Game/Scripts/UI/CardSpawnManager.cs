using System.Collections.Generic;
using Cards;
using Game;
using UnityEngine;

namespace UI
{
    public class CardSpawnManager : MonoBehaviour
    {
        [SerializeField] 
        private EntityRarities playerRarities;
        [SerializeField]
        private GameObject emptyCard;

        private readonly List<GameObject> _cardSpawners = new();
        private readonly List<CardView> _spawnedCards = new();
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
                    SpawnEmptyCard(cardSpawner);
                    continue;
                }
                
                List<CardView> selectedCards = cards.FindAll(c => currentRarity.Name == c.Card.rarity.Name);
                
                if (selectedCards.Count <= 0)
                {
                    SpawnEmptyCard(cardSpawner);
                    continue;
                }
                CardView selectedCard = selectedCards[Random.Range(0, selectedCards.Count)];
                Instantiate(selectedCard, cardSpawner.transform);
                //_spawnedCards.Add(selectedCard);
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
        
        private void SpawnEmptyCard(GameObject cardSpawner)
        {
            Instantiate(emptyCard, cardSpawner.transform);
        }
        public void DeleteAllCards()
        {
            foreach (GameObject cardSpawner in _cardSpawners)
                foreach (Transform child in cardSpawner.transform)
                    Destroy(child.gameObject);
        }
    }
}