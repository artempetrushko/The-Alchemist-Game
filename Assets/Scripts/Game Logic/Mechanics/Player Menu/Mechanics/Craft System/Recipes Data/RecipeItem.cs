using System;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    [Serializable]
    public abstract class RecipeItem
    {
        [SerializeField]
        protected ItemData item;
        [SerializeField]
        protected int count;
    }
}