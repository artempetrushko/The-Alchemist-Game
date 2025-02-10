using Newtonsoft.Json;
using UnityEngine;

namespace GameLogic.LootSystem
{
    public class LimitedItemParameter<T> : ItemParameter<T>
    {
        [JsonProperty]
        [SerializeField] private T _maxValue;

        public T MaxValue => _maxValue;

        public new (string paramName, string formattedParamValue) GetFormattedParamInfo()
        {
            var formattedParamValue = $"{Value}/{MaxValue}";
            if (MeasurementTitle != null)
            {
                formattedParamValue += $" {MeasurementTitle.GetLocalizedString()}";
            }
            return (Name.GetLocalizedString(), formattedParamValue);
        }
    }
}