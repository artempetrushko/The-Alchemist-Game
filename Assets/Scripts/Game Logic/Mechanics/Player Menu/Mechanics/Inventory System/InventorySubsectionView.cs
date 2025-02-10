using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventorySubsectionView : MonoBehaviour
{
    [SerializeField]
    private InventoryCategoryView[] categoryViews;

    public InventoryCategoryView[] CategoryViews => categoryViews;

    public ItemCellView[][] AllInventoryCells => categoryViews.SelectMany(categoryView => categoryView.AllItemCells).ToArray();

    public T GetCategoryView<T>() where T : InventoryCategoryView => categoryViews.FirstOrDefault(categoryView => categoryView is T) as T;
}
