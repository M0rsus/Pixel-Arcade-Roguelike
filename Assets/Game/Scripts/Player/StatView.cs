using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class StatView : MonoBehaviour
    {
        [SerializeField]
        private Image imageContainer;
        [SerializeField]
        private TextMeshProUGUI textNameContainer;
        [SerializeField]
        private TextMeshProUGUI textValueContainer;
        
        private ValueType _valueType;
        private StatInt _statInt;
        private StatFloat _statFloat;
        private StatBool _statBool;

        public void Initialize(Sprite statSprite, string statName, StatInt stat)
        {
            imageContainer.sprite = statSprite;
            textNameContainer.text = statName;
            _statInt = stat;
            _valueType = ValueType.Int;
            UpdateStatInt();
        }
        public void Initialize(Sprite statSprite, string statName, StatFloat stat)
        {
            imageContainer.sprite = statSprite;
            textNameContainer.text = statName;
            _statFloat = stat;
            _valueType = ValueType.Float;
            UpdateStatFloat();
        }
        public void Initialize(Sprite statSprite, string statName, StatBool stat)
        {
            imageContainer.sprite = statSprite;
            textNameContainer.text = statName;
            _statBool = stat;
            _valueType = ValueType.Bool;
            UpdateStatBool();
        }

        private void UpdateStatInt()
        {
            textValueContainer.text = $"{_statInt.GetValue().ToString()}";
        }
        private void UpdateStatFloat()
        {
            textValueContainer.text = $"{_statFloat.GetValue().ToString("F2")}";
        }
        private void UpdateStatBool()
        {
            textValueContainer.text = $"{_statBool.GetValue().ToString()}";
        }

        private void OnEnable()
        {
            switch (_valueType)
            {
                case ValueType.Int:
                    if (_statInt == null) return;
                    _statInt.OnUpdated += UpdateStatInt;
                    UpdateStatInt();
                    break;
                case ValueType.Float:
                    if (_statFloat == null) return;
                    _statFloat.OnUpdated += UpdateStatFloat;
                    UpdateStatFloat();
                    break;
                case ValueType.Bool:
                    if (_statBool == null) return;
                    _statBool.OnUpdated += UpdateStatBool;
                    UpdateStatBool();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDisable()
        {
            switch (_valueType)
            {
                case ValueType.Int:
                    if (_statInt != null) _statInt.OnUpdated -= UpdateStatInt;
                    break;
                case ValueType.Float:
                    if (_statBool != null) _statFloat.OnUpdated -= UpdateStatFloat;
                    break;
                case ValueType.Bool:
                    if (_statBool != null) _statBool.OnUpdated -= UpdateStatBool;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private enum ValueType
        {
            Int,
            Float,
            Bool
        }
    }
}