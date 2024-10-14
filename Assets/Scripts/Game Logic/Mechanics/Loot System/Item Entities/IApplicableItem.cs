using GameLogic.Player;

namespace GameLogic.LootSystem
{
    public interface IApplicableItem
    {
        void Apply(PlayerState player);
    }
}