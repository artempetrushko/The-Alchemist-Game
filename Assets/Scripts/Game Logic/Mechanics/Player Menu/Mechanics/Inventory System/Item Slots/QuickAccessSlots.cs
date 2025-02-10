using System.Linq;
using Controls;
using GameLogic.LootSystem;
using Zenject;

namespace GameLogic.PlayerMenu
{
    public class QuickAccessSlots : ItemSlotCollection, ISelectableCollection
    {
        public readonly ItemSlot[] Slots;

        public QuickAccessSlots(int slotsCount, SignalBus signalBus)
        {
            Slots = new ItemSlot[slotsCount]
                .Select(slot => new ItemSlot(signalBus))
                .ToArray();
        }

        public PlayerInputActionMap InputActionMap => throw new System.NotImplementedException();

        public IPlayerMenuInteractable GetStartSelectedElement(PlayerMenuNavigationStartCondition startCondition = PlayerMenuNavigationStartCondition.Default)
        {
            return startCondition switch
            {
                PlayerMenuNavigationStartCondition.TransitionFromRightCollection => Slots[^1],
                PlayerMenuNavigationStartCondition.TransitionFromTopCollection or PlayerMenuNavigationStartCondition.TransitionFromBottomCollection => Slots[Slots.Length / 2 + 1],
                _ => Slots[0]
            };
        }

        public bool TryPlaceItem(Item item)
        {
            return false;
        }
    }
}