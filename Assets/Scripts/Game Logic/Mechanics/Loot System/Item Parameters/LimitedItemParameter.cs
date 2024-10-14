namespace GameLogic.LootSystem
{
    public class LimitedItemParameter<T> : ItemParameter<T>
    {
        public T MaxValue;

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