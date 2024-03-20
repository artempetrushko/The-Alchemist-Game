using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class RecipesNavigation : PlayerMenuSubsectionNavigation
{
    [Space, SerializeField]
    private RecipesSectionView recipesSection;

    private RecipesSectionElementView currentSelectedElement;
    private int? selectedRecipeCategoryNumber;
    private int? selectedRecipeNumber;

    private RecipesCategoryView[] CategoryViews => recipesSection.RecipeCategories;
    private RecipeButton[] RecipeButtons => SelectedRecipeCategoryNumber != null 
        ? recipesSection.RecipeCategories[SelectedRecipeCategoryNumber.Value - 1].RecipeButtons 
        : null;
    private int? SelectedRecipeCategoryNumber
    {
        get => selectedRecipeCategoryNumber;
        set
        {
            SelectRecipeSectionElement(ref selectedRecipeCategoryNumber, value, CategoryViews);
            SelectedRecipeNumber = null;
        }
    }
    private int? SelectedRecipeNumber
    {
        get => selectedRecipeNumber;
        set
        { 
            SelectRecipeSectionElement(ref selectedRecipeNumber, value, RecipeButtons);
        }
    }

    public override void StartNavigation(SubsectionNavigationStartCondition condition = SubsectionNavigationStartCondition.Default)
    {
        SelectedRecipeCategoryNumber = 1;
    }

    public override void Navigate(Vector2 inputValue)
    {
        if (inputValue.x == 1)
        {
            parentSection.SetCurrentSubsection(rightNeighboringSubsection, SubsectionNavigationStartCondition.TransitionFromLeftSubsection);
        }
        else if (Mathf.Abs(inputValue.y) == 1)
        {
            switch (currentSelectedElement)
            {
                case RecipesCategoryView recipesCategory:
                    switch (inputValue.y)
                    {
                        case 1:
                            if (SelectedRecipeCategoryNumber > 1)
                            {
                                SelectedRecipeCategoryNumber--;
                                if (CategoryViews[SelectedRecipeCategoryNumber.Value - 1].IsOpened)
                                {
                                    SelectedRecipeNumber = RecipeButtons.Length;
                                }
                            }
                            break;

                        case -1:
                            if (recipesCategory.IsOpened)
                            {
                                SelectedRecipeNumber = 1;
                            }
                            else
                            {
                                SelectedRecipeCategoryNumber++;
                            }
                            break;
                    }
                    break;

                case RecipeButton:
                    var newRecipeNumber = SelectedRecipeNumber - (int)inputValue.y;
                    if (newRecipeNumber >= 1 && newRecipeNumber <= RecipeButtons.Length)
                    {
                        SelectedRecipeNumber = newRecipeNumber;
                    }
                    else
                    {
                        SelectedRecipeCategoryNumber += newRecipeNumber.Value > RecipeButtons.Length ? 1 : 0;
                    }
                    break;
            }
        }
    }

    public override void StopNavigation() => EventSystem.current.SetSelectedGameObject(null);

    public override void PressSelectedElement()
    {
        currentSelectedElement.InvokeLinkedAction();
        if (currentSelectedElement is RecipesCategoryView recipesCategory && recipesCategory.IsOpened)
        {
            SelectedRecipeNumber = 1;
        }
    }

    private void SelectRecipeSectionElement(ref int? selectedElementNumber, int? newElementNumber, RecipesSectionElementView[] recipesSectionElements)
    {
        if (newElementNumber == null)
        {
            selectedElementNumber = newElementNumber;
        }
        else
        {
            selectedElementNumber = Mathf.Clamp(newElementNumber.Value, 1, recipesSectionElements.Length);
            currentSelectedElement = recipesSectionElements[selectedElementNumber.Value - 1];
            currentSelectedElement.Select();
        }       
    }
}
