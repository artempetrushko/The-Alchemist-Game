using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipesSectionView : MonoBehaviour
{
    [SerializeField]
    private GameObject recipesCategoriesContainer;
    [SerializeField]
    private RecipesCategoryView recipesCategoryViewPrefab;

    public RecipesCategoryView[] RecipeCategories => recipesCategoriesContainer.GetComponentsInChildren<RecipesCategoryView>();

    public void CreateRecipeCategories(RecipeData[] recipes, Action<RecipeData> recipeButtonsAction)
    {
        var recipesDictionary = recipes.GroupBy(recipe => recipe.LocalizedCategoryTitle).ToDictionary(group => group.Key, group => group.ToArray());
        foreach (var recipesGroup in recipesDictionary)
        {
            var newCategoryView = Instantiate(recipesCategoryViewPrefab, recipesCategoriesContainer.transform);
            newCategoryView.SetInfo(recipesGroup.Key, recipesGroup.Value, recipeButtonsAction);
        }
    }

    public void DeleteRecipeCategories()
    {
        for (var i = recipesCategoriesContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(recipesCategoriesContainer.transform.GetChild(i).gameObject);
        }
    }
}
