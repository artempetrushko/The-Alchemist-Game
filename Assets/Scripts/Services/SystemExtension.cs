using Newtonsoft.Json;

namespace GameLogic.LootSystem
{
    public static class SystemExtension
    {
        public static T Clone<T>(this T source)
        {
            var serializedSource = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serializedSource);
        }
    }
}