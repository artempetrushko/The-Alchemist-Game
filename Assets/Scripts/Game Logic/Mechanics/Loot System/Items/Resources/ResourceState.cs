namespace GameLogic.LootSystem
{
    public abstract class ResourceState : StackableItemState
    {
        protected ResourceState(ResourceData item, int itemsCount = 0) : base(item, itemsCount) { }
    }
}