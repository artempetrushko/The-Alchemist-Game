namespace GameLogic.LootSystem
{
    public interface IFormattedItemParameter
    {
        (string paramName, string formattedParamValue) GetFormattedParamInfo();
    }
}