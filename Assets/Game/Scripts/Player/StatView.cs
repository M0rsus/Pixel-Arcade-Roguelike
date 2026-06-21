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
            UpdateStat();
        }
        public void Initialize(Sprite statSprite, string statName, StatFloat stat)
        {
            imageContainer.sprite = statSprite;
            textNameContainer.text = statName;
            _statFloat = stat;
            _valueType = ValueType.Float;
            UpdateStat();
        }
        public void Initialize(Sprite statSprite, string statName, StatBool stat)
        {
            imageContainer.sprite = statSprite;
            textNameContainer.text = statName;
            _statBool = stat;
            _valueType = ValueType.Bool;
            UpdateStat();
        }

        private void UpdateStat()
        {
            string newValue = _valueType switch
            {
                ValueType.Int => _statInt.GetValue().ToString(),
                ValueType.Float => _statFloat.GetValue().ToString("F2"),
                ValueType.Bool => _statBool.GetValue().ToString(),
                _ => throw new ArgumentOutOfRangeException()
            };
            textValueContainer.text = $"{newValue}";
        }

        private void OnEnable()
        {
            switch (_valueType)
            {
                case ValueType.Int:
                    _statInt.OnUpdated += UpdateStat;
                    break;
                case ValueType.Float:
                    _statFloat.OnUpdated += UpdateStat;
                    break;
                case ValueType.Bool:
                    _statBool.OnUpdated += UpdateStat;
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
                    _statInt.OnUpdated -= UpdateStat;
                    break;
                case ValueType.Float:
                    _statFloat.OnUpdated -= UpdateStat;
                    break;
                case ValueType.Bool:
                    _statBool.OnUpdated -= UpdateStat;
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