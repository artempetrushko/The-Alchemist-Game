using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RecipeButton : RecipesSectionElementView
{
    [SerializeField]
    private TMP_Text recipeDescription;
    [SerializeField]
    private Image recipeResultItemIcon;

    public void SetInfo(RecipeData recipe, Action<RecipeData> buttonAction)
    {
        title.text = recipe.Title;
        recipeDescription.text = recipe.Description;
        recipeResultItemIcon.sprite = recipe.RecipeIcon;
        buttonComponent.onClick.AddListener(() => buttonAction(recipe));
    }
}
