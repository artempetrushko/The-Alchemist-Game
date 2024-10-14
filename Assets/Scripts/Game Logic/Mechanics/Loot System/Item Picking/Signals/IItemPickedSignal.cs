using GameLogic.LootSystem;

namespace GameLogic.EnvironmentExploration
{
    public interface IItemPickedSignal
	{
		ItemState Item { get; }
	}
}