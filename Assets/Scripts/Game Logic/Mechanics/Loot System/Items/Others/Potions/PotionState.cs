using System.Collections.Generic;

namespace GameLogic.LootSystem
{
    public class PotionState : StackableItemState
    {
        public PotionState(PotionData potionData) : base(potionData) { }

        public override object Clone() => new PotionState(BaseParams as PotionData)
        {
            //ItemData = ItemData,
            ItemsCount = ItemsCount,
            MaxStackItemsCount = MaxStackItemsCount,
            Description = Description,
            Aspects = Aspects,
            CastingDamage = CastingDamage,
            Effects = Effects,
        };

        public override Dictionary<string, string> GetItemParams()
        {
            return new Dictionary<string, string>()
        {
            { "Сила действия", (BaseParams as PotionData).EffectPower.ToString() },
            { "Продолжительность", (BaseParams as PotionData).EffectDurationInSeconds + " сек." }
        };
        }

        public EffectApplyingActions GetEffectApplyingActions(ABC_StateManager effectReceiver) => PotionEffects.GetEffectApplyingActions(this, effectReceiver);
    }
}