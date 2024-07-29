using System.Collections;
using System.Collections.Generic;
using UI.PlayerMenu;
using UnityEngine;
using UnityEngine.UI;

public class CraftingItemTemplateView : MonoBehaviour
{
    [SerializeField]
    private Image craftingItemIcon;
    [SerializeField]
    private GameObject ingredientCellsContainer;
    [SerializeField]
    private CraftingItemTemplateNavigation subsectionNavigation;

    public IngredientCellView[] IngredientCells => ingredientCellsContainer.GetComponentsInChildren<IngredientCellView>();
    public CraftingItemTemplateNavigation SubsectionNavigation => subsectionNavigation;

    public void SetInfo(Sprite craftingItemIcon, IngredientCellsTemplateView ingredientCellsTemplatePrefab)
    {
        this.craftingItemIcon.gameObject.SetActive(true);
        this.craftingItemIcon.sprite = craftingItemIcon;
        var ingredientCellsTemplate = Instantiate(ingredientCellsTemplatePrefab, ingredientCellsContainer.transform);
        foreach (var ingredientCell in ingredientCellsTemplate.IngredientCells)
        {
            ingredientCell.transform.SetParent(ingredientCellsContainer.transform);
        }
        Destroy(ingredientCellsTemplate);
    }

    public void Clear()
    {
        craftingItemIcon.sprite = null;
        craftingItemIcon.gameObject.SetActive(false);
        for (var i = ingredientCellsContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(ingredientCellsContainer.transform.GetChild(i).gameObject);
        }
        ingredientCellsContainer.transform.DetachChildren();
    }

    private void OnEnable()
    {
        craftingItemIcon.gameObject.SetActive(false);
    }
}
