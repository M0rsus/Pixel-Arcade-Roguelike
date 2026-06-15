using System;
using Game;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SliderView : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        [SerializeField] [HideIf("view", View.None)]
        private TextMeshProUGUI text;
        [SerializeField] 
        private View view;
        [SerializeField] [ShowIf("view", View.Labeled)]
        private string prefixedText;

        private ValueType _valueType;
        private StatInt _currentInt;
        private StatInt _maxInt;
        private StatFloat _currentFloat;
        private StatFloat _maxFloat;

        public void Initialize(StatInt current, StatInt max)
        {
            _currentInt = current;
            _maxInt = max;
            _valueType = ValueType.Int;
        }
        public void Initialize(StatFloat current, StatFloat max)
        {
            _currentFloat = current;
            _maxFloat = max;
            _valueType = ValueType.Float;
        }

        private void Update()
        {
            var current = _valueType switch
            {
                ValueType.Int => _currentInt.GetValue(),
                ValueType.Float => _currentFloat.GetValue(),
                _ => throw new ArgumentOutOfRangeException()
            };
            var max = _valueType switch
            {
                ValueType.Int => _maxInt.GetValue(),
                ValueType.Float => _maxFloat.GetValue(),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            slider.value = max > 0 ? (float)current / max : 0f;
            switch (view)
            {
                case View.Ratio:
                    text.SetText($"{current}/{max}");
                    break;
                case View.Labeled:
                    text.SetText($"{prefixedText}: {current}");
                    break;
                case View.None:
                    break;
                default:
                    goto case View.None;
            }
        }

        private enum View
        {
            Ratio,
            Labeled,
            None
        }

        private enum ValueType
        {
            Int,
            Float
        }
    }
}