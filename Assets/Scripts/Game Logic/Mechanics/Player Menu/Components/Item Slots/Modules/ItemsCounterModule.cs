using GameLogic.LootSystem;
using TMPro;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
	public class ItemsCounterModule : ItemSlotModule
	{
		[SerializeField] private TMP_Text _itemsCounter;

        public override void UpdateDisplayedInfo(ItemState item)
        {
            if (item is StackableItemState stackableItem)
            {
                SetVisible(true);
                _itemsCounter.text = $"x{stackableItem.Count.Value}";
            }
            else
            {
                SetVisible(false);
            }
        }
    }
}