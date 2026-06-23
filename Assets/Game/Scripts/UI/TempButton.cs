using System;
using UnityEngine;

namespace UI
{
    public class TempButton : MonoBehaviour
    {
        [SerializeField] private GameObject spawn;
        [SerializeField] private GameObject card;
        [SerializeField] private CardSpawn cardSpawn;
        public static event Action OnCardCreate;

        public void CreateCard()
        {
            //cardSpawn.Instantiate(card);
            OnCardCreate?.Invoke();
        }
    }
}