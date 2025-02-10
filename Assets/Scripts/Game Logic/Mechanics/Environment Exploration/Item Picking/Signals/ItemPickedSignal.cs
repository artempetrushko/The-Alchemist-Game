using GameLogic.LootSystem;

namespace GameLogic.EnvironmentExploration
{
    public abstract class ItemPickedSignal
	{
		public readonly Item Item;

		public ItemPickedSignal(Item item)
		{
			Item = item;
		}
	}
}