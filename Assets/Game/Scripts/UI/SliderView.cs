using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SliderView : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private TextMeshProUGUI text;

        private StatInt _current;
        private StatInt _max;

        public void Initialize(StatInt current, StatInt max)
        {
            _current = current;
            _max = max;
        }
        
    }
}