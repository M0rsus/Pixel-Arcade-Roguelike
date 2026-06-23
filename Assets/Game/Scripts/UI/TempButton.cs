using System;
using UnityEngine;

namespace UI
{
    public class TempButton : MonoBehaviour
    {
        [SerializeField] private GameObject spawn;
        [SerializeField] private GameObject card;
        [SerializeField] private CardSpawn cardSpawn;
        [SerializeField] private UnityEngine.UI.Image image;
        public static event Action OnCardCreate;

        public void CreateCard()
        {
            //cardSpawn.Instantiate(card);
            OnCardCreate?.Invoke();
        }
    }
}