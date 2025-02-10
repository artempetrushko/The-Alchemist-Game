using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RecipesCategoryView : RecipesSectionElementView
{
    [SerializeField]
    private RecipeButton recipeButtonPrefab;
    [SerializeField]
    private GameObject recipeButtonsContainer;

    public bool IsOpened => recipeButtonsContainer.activeInHierarchy;
    public RecipeButton[] RecipeButtons => recipeButtonsContainer.GetComponentsInChildren<RecipeButton>();

    public void SetInfo(string categoryTitle, RecipeData[] recipes, Action<RecipeData> recipeButtonsAction)
    {
        title.text = categoryTitle;
        buttonComponent.onClick.AddListener(() => recipeButtonsContainer.SetActive(!recipeButtonsContainer.activeInHierarchy));
        foreach (var recipe in recipes)
        {
            var recipeButton = Instantiate(recipeButtonPrefab, recipeButtonsContainer.transform);
            recipeButton.SetInfo(recipe, recipeButtonsAction);
        }
    }

    public void SetRecipeButtonsActive(bool isActive) => recipeButtonsContainer.SetActive(isActive);
}
