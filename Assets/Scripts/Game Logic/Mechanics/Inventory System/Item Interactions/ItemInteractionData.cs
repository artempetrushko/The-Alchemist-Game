using System;
using UnityEngine;

namespace GameLogic
{
    [Serializable]
    public class ItemInteractionData
    {
        [SerializeField] private ItemInteractionType _interaction;
        [SerializeField] private string _displayedName;
        [SerializeField] private Sprite _icon;

        public ItemInteractionType Interaction => _interaction;
        public string DisplayedName => _displayedName;
        public Sprite Icon => _icon;
    }
}