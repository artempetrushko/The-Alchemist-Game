using System;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class CraftInventorySubsectionView : MonoBehaviour
    {
        [SerializeField]
        private InventoryItemsCategoryButton categoryButtonPrefab;
        [SerializeField]
        private GameObject categoryButtonsContainer;
        [Space, SerializeField]
        private GameObject categoryViewsContainer;

        public InventoryItemsCategoryButton[] CreateCategoryButtons(InventoryCategoryData[] inventoryCategoryDatas, Action<InventoryCategoryView> createCategoryViewAction)
        {
            var categoryButtons = new InventoryItemsCategoryButton[inventoryCategoryDatas.Length];
            for (var i = 0; i < inventoryCategoryDatas.Length; i++)
            {
                var orderNumber = i;
                categoryButtons[orderNumber] = Instantiate(categoryButtonPrefab, categoryButtonsContainer.transform);
                categoryButtons[orderNumber].SetInfo(inventoryCategoryDatas[orderNumber].CategoryIcon, () => createCategoryViewAction(inventoryCategoryDatas[orderNumber].CategoryViewPrefab));
            }
            return categoryButtons;
        }

        public InventoryCategoryView CreateCategoryView(InventoryCategoryView categoryViewPrefab) => Instantiate(categoryViewPrefab, categoryViewsContainer.transform);

        public void ClearCategoryView()
        {
            if (categoryViewsContainer.transform.childCount > 0)
            {
                Destroy(categoryViewsContainer.transform.GetChild(0).gameObject);
            }
        }
    }
}