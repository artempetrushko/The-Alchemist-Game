using System.Linq;
using Controls;
using Zenject;

namespace GameLogic.PlayerMenu.Craft
{
    public class EnergySlots : ItemSlotCollection, ISelectableCollection
    {
        public readonly EnergySlot[] Slots;

        public PlayerInputActionMap InputActionMap => throw new System.NotImplementedException();

        public EnergySlots(ItemSlotView[] slotViews, SignalBus signalBus)
        {
            Slots = slotViews
                .Select(slotView => new EnergySlot(signalBus) { View = slotView })
                .ToArray();
        }

        public IPlayerMenuInteractable GetStartSelectedElement(PlayerMenuNavigationStartCondition startCondition = PlayerMenuNavigationStartCondition.Default)
        {
            throw new System.NotImplementedException();
        }
    }
}