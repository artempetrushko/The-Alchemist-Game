using GameLogic.LootSystem;

namespace GameLogic.EnvironmentExploration
{
    public abstract class ItemPickedSignal
	{
		public readonly ItemState Item;

		public ItemPickedSignal(ItemState item)
		{
			Item = item;
		}
	}
}