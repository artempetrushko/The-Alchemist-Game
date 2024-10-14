using GameLogic.LootSystem;
using Zenject;

namespace GameLogic.PlayerMenu.Craft
{
    public class IngredientSlot : ItemSlot
    {
        private SignalBus _signalBus;

        public IngredientSlot(SignalBus signalBus) : base(signalBus) { }

        public override void PlaceNewItem(ItemState item)
        {
            base.PlaceNewItem(item);

            _signalBus.Fire(new IngredientSlotItemChangedSignal(this));
        }

        public void UpdateRequiredItemsCounter(int? requiredItemsCount) { }
    }
}