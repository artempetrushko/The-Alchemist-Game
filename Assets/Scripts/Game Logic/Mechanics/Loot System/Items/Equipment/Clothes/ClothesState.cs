using System.Collections.Generic;

namespace GameLogic.LootSystem
{
    public class ClothesState : EquipmentState
    {
        public int Defence { get; set; }

        public ClothesState(ClothesData clothes) : base(clothes)
        {
            Defence = clothes.BaseDefence;
        }

        public override object Clone() => new ClothesState(BaseParams as ClothesData)
        {
            //ItemData = ItemData,
            Description = Description,
            Endurance = Endurance,
            Defence = Defence,
        };

        public override Dictionary<string, string> GetItemParams()
        {
            return new();
        }
    }
}