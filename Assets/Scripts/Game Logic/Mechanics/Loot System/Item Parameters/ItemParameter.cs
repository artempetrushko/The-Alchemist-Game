using System;
using UnityEngine.Localization;

namespace GameLogic.LootSystem
{
    [Serializable]
    public class ItemParameter<T> : IFormattedItemParameter
    {
        public LocalizedString Name;
        public T Value;
        public LocalizedString MeasurementTitle;

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