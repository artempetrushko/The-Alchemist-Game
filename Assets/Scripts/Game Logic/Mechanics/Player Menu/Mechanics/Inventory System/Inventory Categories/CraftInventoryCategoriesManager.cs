using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameLogic.PlayerMenu
{
    public class CraftInventoryCategoriesManager : MonoBehaviour
    {
        [SerializeField]
        private InventoryCategoryData[] categoryDatas;
        [SerializeField]
        private InventoryManager inventoryManager;

        private CraftInventorySubsectionView craftInventorySubsectionView;
        private PlayerMenuSectionNavigation categoriesParentSection;
        private InventoryItemsCategoryButton[] categoryButtons;
        private InventoryCategoryView currentInventoryCategoryView;
        private int currentItemCellsCategoryNumber;

        private InventoryCategoryView CurrentInventoryCategoryView
        {
            get => currentInventoryCategoryView;
            set
            {
                if (currentInventoryCategoryView != value)
                {
                    if (currentInventoryCategoryView != null)
                    {
                        craftInventorySubsectionView.ClearCategoryView();
                    }
                    currentInventoryCategoryView = craftInventorySubsectionView.CreateCategoryView(value);
                    inventoryManager.InitializeSelectedCategory(currentInventoryCategoryView);
                    currentInventoryCategoryView.SetParentSectionNavigation(categoriesParentSection);
                }
            }
        }
        private int CurrentItemCellsCategoryNumber
        {
            get => currentItemCellsCategoryNumber;
            set
            {
                if (value >= 1 && value <= categoryButtons.Length)
                {
                    if (currentItemCellsCategoryNumber != 0)
                    {
                        categoryButtons[currentItemCellsCategoryNumber - 1].SetButtonState(false);
                    }
                    currentItemCellsCategoryNumber = value;
                    if (currentItemCellsCategoryNumber != 0)
                    {
                        categoryButtons[currentItemCellsCategoryNumber - 1].SetButtonState(true);
                    }
                    CurrentInventoryCategoryView = categoryDatas[currentItemCellsCategoryNumber - 1].CategoryViewPrefab;
                }
            }
        }

        public void Initialize(CraftInventorySubsectionView inventorySubsectionView, PlayerMenuSectionNavigation parentSection)
        {
            craftInventorySubsectionView = inventorySubsectionView;
            categoriesParentSection = parentSection;
            categoryButtons = craftInventorySubsectionView.CreateCategoryButtons(categoryDatas, (categoryView) =>
            {
                CurrentItemCellsCategoryNumber = categoryDatas.Select(categoryData => categoryData.CategoryViewPrefab).ToList().IndexOf(categoryView) + 1;
            });
            CurrentItemCellsCategoryNumber = 1;
        }

        public void ChangeItemsCategory(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                CurrentItemCellsCategoryNumber += (int)context.ReadValue<Vector2>().x;
            }
        }
    }
}