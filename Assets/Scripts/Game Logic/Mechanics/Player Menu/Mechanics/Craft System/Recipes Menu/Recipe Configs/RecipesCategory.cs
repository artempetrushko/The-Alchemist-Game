using System;
using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic.PlayerMenu.Craft
{
    [Serializable]
    public class RecipesCategory
    {
        [SerializeField] private LocalizedString _categoryTitle;
        [SerializeField] private Recipe[] _recipes;

        public LocalizedString CategoryTitle => _categoryTitle;
        public Recipe[] Recipes => _recipes;
    }
}