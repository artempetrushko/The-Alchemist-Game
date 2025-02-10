using GameLogic.LootSystem;
using Zenject;

namespace GameLogic.PlayerMenu.Inventory
{
    public class ClothesSlot : ItemSlot, ISpecialItemSlot
    {
        public ClothesSlot(SignalBus signalBus) : base(signalBus)
        {
        }

        public bool CheckItemRequirementsCompliance(Item item)
        {
            throw new System.NotImplementedException();
        }
    }
}