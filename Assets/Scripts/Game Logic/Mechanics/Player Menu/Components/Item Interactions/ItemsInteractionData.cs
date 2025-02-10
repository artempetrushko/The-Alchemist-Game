using System;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    [Serializable]
    public class ItemsInteractionData
    {
        [SerializeField]
        private ItemInteraction interaction;
        [SerializeField]
        private string displayedName;
        [SerializeField]
        private Sprite icon;
        [SerializeField]
        private ItemsInteractionModule interactionModule;

        public ItemInteraction Interaction => interaction;
        public string DisplayedName => displayedName;
        public Sprite Icon => icon;
        public ItemsInteractionModule InteractionModule => interactionModule;
    }
}