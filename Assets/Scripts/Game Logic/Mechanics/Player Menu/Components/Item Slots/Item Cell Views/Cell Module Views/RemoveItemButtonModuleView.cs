using GameLogic.LootSystem;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
    public class RemoveItemButtonModuleView : ItemCellModuleView
    {
        [SerializeField]
        private Button removeItemButton;

        public override void SetActive(bool isActive)
        {
            removeItemButton.gameObject.SetActive(isActive);
        }

        public override bool TryEnableWithNewItem(ItemState newItem)
        {
            return false;
        }

        public override void UpdateContent(ItemState attachedItem)
        {

        }
    }
}