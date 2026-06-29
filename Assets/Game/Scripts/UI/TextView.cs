using Game;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TextView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;
        [SerializeField] 
        private View view;
        [SerializeField] [ShowIf("view", View.Labeled)]
        private string prefixedText;
        
        private StatInt _current;
        private StatInt _max;

        public void Initialize(StatInt current, StatInt max)
        {
            _current = current;
            _max = max;
        }

        public void Initialize(StatInt current)
        {
            _current = current;
        }
        private void Update()
        {
            switch (view)
            {
                case View.Ratio:
                    text.SetText($"{_current.Value}/{_max.Value}");
                    break;
                case View.Labeled:
                    text.SetText($"{prefixedText}{_current.Value}");
                    break;
                default:
                    goto case View.Ratio;
            }
        }
        private enum View
        {
            Ratio,
            Labeled
        }
    }
}