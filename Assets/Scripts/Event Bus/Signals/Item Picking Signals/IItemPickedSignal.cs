using GameLogic.LootSystem;

namespace EventBus
{
    public interface IItemPickedSignal
	{
		Item Item { get; }
	}
}