using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public abstract class InventoryCategoryView : MonoBehaviour
    {
        [SerializeField]
        private ItemCellView itemCellPrefab;
        [SerializeField]
        protected GameObject itemCellsContainer;

        public virtual ItemCellView[][] AllItemCells => new ItemCellView[][] { MainItemCells };
        public ItemCellView[] MainItemCells => itemCellsContainer.GetComponentsInChildren<ItemCellView>();

        public virtual void FillItemCellsContainer(int cellsCount)
        {
            for (var i = 0; i < cellsCount; i++)
            {
                Instantiate(itemCellPrefab, itemCellsContainer.transform);
            }
        }

        public void SetParentSectionNavigation(PlayerMenuSectionNavigation parentSection)
        {
            var subsections = GetComponentsInChildren<PlayerMenuSubsectionNavigation>();
            foreach (var subsection in subsections)
            {
                subsection.ParentSection = parentSection;
            }
            parentSection.SetCurrentSubsection(subsections[0]);
        }
    }
}