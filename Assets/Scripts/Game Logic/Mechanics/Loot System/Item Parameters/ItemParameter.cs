using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic.LootSystem
{
    [Serializable]
    public class ItemParameter<T> : IFormattedItemParameter
    {
        [JsonProperty]
        [SerializeField] private LocalizedString _name;
        [JsonProperty]
        [SerializeField] private T _value;
        [JsonProperty]
        [SerializeField] private LocalizedString _measurementTitle;

        public LocalizedString Name => _name;
        public LocalizedString MeasurementTitle => _measurementTitle;
        public T Value
        {
            get => _value;
            set
            {
                if (!_value.Equals(value))
                {
                    _value = value;
                }
            }
        }

        public (string paramName, string formattedParamValue) GetFormattedParamInfo()
        {
            var formattedParamValue = Value.ToString();
            if (MeasurementTitle != null)
            {
                formattedParamValue += $" {MeasurementTitle.GetLocalizedString()}";
            }
            return (Name.GetLocalizedString(), formattedParamValue);
        }
    }
}