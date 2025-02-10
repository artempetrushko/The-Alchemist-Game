using GameLogic.LootSystem;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
    public class InnerBackgroundModuleView : ItemCellModuleView
    {
        [SerializeField]
        protected Image backgroundIcon;

        public override void SetActive(bool isActive)
        {
            backgroundIcon.gameObject.SetActive(isActive);
        }

        public override bool TryEnableWithNewItem(ItemState newItem)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateContent(ItemState attachedItem)
        {
            throw new System.NotImplementedException();
        }
    }
}