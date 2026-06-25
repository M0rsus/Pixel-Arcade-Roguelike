using System;
using Cards;
using UnityEngine;

namespace UI
{
    public class TempButton : MonoBehaviour
    {
        [SerializeField] private GameObject spawn;
        [SerializeField] private GameObject cardSpawn;
        [SerializeField] private UnityEngine.UI.Image image;
        public static event Action OnCardCreate;

        public void CreateCard()
        {
            Instantiate(CardRegistry.Cards[0], cardSpawn.transform);
            OnCardCreate?.Invoke();
        }
    }
}