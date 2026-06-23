using Demo;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "Card", menuName = "Game/Card")]
    public class CardObject : ScriptableObject
    {
        [SerializeReference] [SRDemo]
        public BaseCard card;
    }
}