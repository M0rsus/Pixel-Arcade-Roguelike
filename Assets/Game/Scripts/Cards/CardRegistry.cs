using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Cards
{
    public class CardRegistry : MonoBehaviour
    {
        [SerializeField] 
        private List<GameObject> cardObjects;
        public static List<CardView> Cards { get; } = new();
        private void Awake()
        {
            foreach (GameObject card in cardObjects)
                Cards.Add(card.GetComponent<CardView>());
        }
    }
}