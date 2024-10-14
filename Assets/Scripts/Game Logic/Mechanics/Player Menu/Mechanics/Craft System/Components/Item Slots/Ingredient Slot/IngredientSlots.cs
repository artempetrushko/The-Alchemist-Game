using System.Linq;
using Controls;
using Zenject;

namespace GameLogic.PlayerMenu.Craft
{
    public class IngredientSlots : ItemSlotCollection, ISelectableCollection
    {
        public readonly IngredientSlot[] Slots;

        public PlayerInputActionMap InputActionMap => throw new System.NotImplementedException();

        public IngredientSlots(ItemSlotView[] slotViews, SignalBus signalBus)
        {
            Slots = slotViews
                .Select(slotView => new IngredientSlot(signalBus) { View = slotView })
                .ToArray();
        }

        public IPlayerMenuInteractable GetStartSelectedElement(PlayerMenuNavigationStartCondition startCondition = PlayerMenuNavigationStartCondition.Default)
        {
            throw new System.NotImplementedException();
        }
    }
}