using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic.PlayerMenu
{
    public abstract class ItemsInteraction : ScriptableObject
    {
        [SerializeField] private LocalizedString _displayedName;
        [SerializeField] private Sprite _icon;

        public LocalizedString DisplayedName => _displayedName;
        public Sprite Icon => _icon;
        public ItemSlot StartItemSlot { get; protected set; }

        public abstract bool CheckAvailability();

        public abstract void Activate(ItemSlot selectedItemSlot);
    }
}