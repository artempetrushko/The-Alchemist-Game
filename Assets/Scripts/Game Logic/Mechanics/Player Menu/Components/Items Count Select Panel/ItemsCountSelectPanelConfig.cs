using Controls;
using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic.PlayerMenu
{
    [CreateAssetMenu(fileName = "Items Count Select Panel Config", menuName = "Game Configs/Player Menu/Items Count Select Panel Config")]
    public class ItemsCountSelectPanelConfig : ScriptableObject
    {
        [SerializeField] private PlayerInputActionMap _actionMap;
        [SerializeField] private LocalizedString _actionDescriptionText;
        [SerializeField] private LocalizedString _selectItemsCountActionButtonText;
        [SerializeField] private LocalizedString _selectAllItemsActionButtonText;
        [SerializeField] private LocalizedString _cancelItemsCountSelectionActionButtonText;

        public PlayerInputActionMap ActionMap => _actionMap;
        public LocalizedString ActionDescriptionText => _actionDescriptionText;
        public LocalizedString SelectItemsCountActionButtonText => _selectItemsCountActionButtonText;
        public LocalizedString SelectAllItemsActionButtonText => _selectAllItemsActionButtonText;
        public LocalizedString CancelItemsCountSelectionActionButtonText => _cancelItemsCountSelectionActionButtonText;
    }
}