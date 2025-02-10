using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public abstract class ItemCellModuleView : MonoBehaviour
    {
        public abstract void SetActive(bool isActive);

        public abstract bool TryEnableWithNewItem(ItemState newItem);

        public abstract void UpdateContent(ItemState attachedItem);
    }
}