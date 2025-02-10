using GameLogic.LootSystem;
using Zenject;

namespace GameLogic.PlayerMenu.Inventory
{
    public class WeaponSlot : ItemSlot, ISpecialItemSlot
    {
        public WeaponSlot(SignalBus signalBus) : base(signalBus)
        {
        }

        public bool CheckItemRequirementsCompliance(Item item)
        {
            throw new System.NotImplementedException();
        }
    }
}