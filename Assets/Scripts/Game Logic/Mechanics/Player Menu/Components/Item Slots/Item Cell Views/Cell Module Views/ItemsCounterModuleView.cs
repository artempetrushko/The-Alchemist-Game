using GameLogic.LootSystem;
using TMPro;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class ItemsCounterModuleView : ItemCellModuleView
    {
        [SerializeField]
        private TMP_Text itemsCounter;

        public override void SetActive(bool isActive)
        {
            itemsCounter.gameObject.SetActive(isActive);
        }

        public override bool TryEnableWithNewItem(ItemState newItem)
        {
            if (newItem is StackableItemState)
            {
                SetActive(true);
                return true;
            }
            return false;
        }

        public override void UpdateContent(ItemState attachedItem)
        {
            if (attachedItem is StackableItemState stackableItem)
            {
                itemsCounter.text = "x" + stackableItem.ItemsCount.ToString();
            }
        }
    }
}