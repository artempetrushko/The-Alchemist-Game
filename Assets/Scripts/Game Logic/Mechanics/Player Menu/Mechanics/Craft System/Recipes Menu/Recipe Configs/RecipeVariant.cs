using System;
using UnityEngine;

namespace GameLogic.PlayerMenu.Craft
{
    [Serializable]
    public class RecipeVariant
    {
        [SerializeField] private RecipeItem[] _ingredients;
        [SerializeField] private RecipeItem _resultItem;

        public RecipeItem[] Ingredients => _ingredients;
        public RecipeItem ResultItem => _resultItem;
    }
}