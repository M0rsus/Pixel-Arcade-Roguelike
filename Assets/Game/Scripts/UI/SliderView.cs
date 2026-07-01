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
        [SerializeField] [HideIf("view", View.None)]
        private FloatPrecision floatPrecision;
        [SerializeField] [ShowIf("view", View.Labeled)]
        private string prefixedText;

        private int _digits;
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
            _maxInt.OnUpdated += UpdateSlider;
            _currentInt.OnUpdated += UpdateSlider;
            UpdateSlider();
        }
        public void Initialize(StatFloat current, StatFloat max)
        {
            _currentFloat = current;
            _maxFloat = max;
            _valueType = ValueType.Float;
            _digits = floatPrecision switch
            {
                FloatPrecision.Two => 2,
                FloatPrecision.One => 1,
                FloatPrecision.Zero => 0,
                _ => 2
            };
            _maxFloat.OnUpdated += UpdateSlider;
            _currentFloat.OnUpdated += UpdateSlider;
            UpdateSlider();
        }

        private void UpdateSlider()
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
                    text.SetText($"{MathF.Round(current, _digits)}/{MathF.Round(max, _digits)}");
                    break;
                case View.Labeled:
                    text.SetText($"{prefixedText}{MathF.Round(current, _digits)}");
                    break;
                case View.None:
                    break;
                default:
                    goto case View.None;
            }
        }

        private void OnDestroy()
        {
            if (_maxInt != null) _maxInt.OnUpdated -= UpdateSlider;
            if (_maxFloat != null) _maxFloat.OnUpdated -= UpdateSlider;
            if (_currentInt != null) _currentInt.OnUpdated -= UpdateSlider;
            if (_currentFloat != null) _currentFloat.OnUpdated -= UpdateSlider;
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

        private enum FloatPrecision
        {
            Two,
            One,
            Zero
        }
    }
}