using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    [Serializable]
    public class RecipeVariant
    {
        [SerializeField]
        private List<RecipeIngredient> ingredients = new();
        [SerializeField]
        private RecipeResultItem resultItem;

        public List<RecipeIngredient> Ingredients => ingredients;
        public RecipeResultItem ResultItem => resultItem;

        public bool CheckRecipeVariantMatching(List<ItemState> selectedItems) => CheckIngredientsRequirements(selectedItems);

        public bool CheckCraftingAvailability(List<ItemState> selectedItems) => CheckIngredientsRequirements(selectedItems, true);

        private bool CheckIngredientsRequirements(List<ItemState> selectedItems, bool isCountMatchingRequired = false)
        {
            return selectedItems
                .Where(item =>
                {
                    if (item == null)
                    {
                        return false;
                    }
                    var accordingIngredient = ingredients[selectedItems.IndexOf(item)];
                    return accordingIngredient.CheckSelectedItemMatching(item, isCountMatchingRequired);
                })
                .Count() == selectedItems.Count;
        }
    }
}