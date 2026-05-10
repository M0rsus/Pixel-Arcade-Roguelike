using System;
using UnityEngine;

namespace UI
{
    public class TempButton : MonoBehaviour
    {
        [SerializeField] private GameObject spawn;
        [SerializeField] private GameObject card;
        public static event Action OnCardCreate;

        public void CreateCard()
        {
            Instantiate(card, spawn.transform);
            OnCardCreate?.Invoke();
        }
    }
}