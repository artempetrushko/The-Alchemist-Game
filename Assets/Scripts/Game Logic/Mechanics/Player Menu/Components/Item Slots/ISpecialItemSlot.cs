using GameLogic.LootSystem;

namespace GameLogic.PlayerMenu
{
    public interface ISpecialItemSlot
    {
        bool CheckItemRequirementsCompliance(Item item);
    }
}