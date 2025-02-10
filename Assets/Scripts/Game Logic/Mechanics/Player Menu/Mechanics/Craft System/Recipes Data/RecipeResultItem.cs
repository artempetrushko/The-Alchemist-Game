using System;
using GameLogic.LootSystem;

namespace GameLogic.PlayerMenu
{
    [Serializable]
    public class RecipeResultItem : RecipeItem
    {
        public ItemState GetResultItemState()
        {
            var resultItemState = item.GetItemState();
            if (resultItemState is StackableItemState)
            {
                (resultItemState as StackableItemState).ItemsCount = count;
            }
            return resultItemState;
        }
    }
}