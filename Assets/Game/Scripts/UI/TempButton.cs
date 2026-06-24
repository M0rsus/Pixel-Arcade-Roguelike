using System;
using UnityEngine;

namespace UI
{
    public class TempButton : MonoBehaviour
    {
        [SerializeField] private GameObject spawn;
        [SerializeField] private GameObject card;
        [SerializeField] private GameObject cardSpawn;
        [SerializeField] private UnityEngine.UI.Image image;
        public static event Action OnCardCreate;

        public void CreateCard()
        {
            Instantiate(card, cardSpawn.transform);
            OnCardCreate?.Invoke();
        }
    }
}