using GameLogic.LootSystem;

namespace GameLogic.PlayerMenu
{
    public interface ISpecialItemSlot
    {
        bool CheckItemRequirementsCompliance(ItemState item);
    }
}