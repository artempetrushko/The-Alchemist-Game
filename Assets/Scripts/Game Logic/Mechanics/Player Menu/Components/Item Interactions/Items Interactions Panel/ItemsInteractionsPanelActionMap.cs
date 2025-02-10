using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

namespace Controls
{
    [CreateAssetMenu(fileName = "Items Interactions Panel Action Map", menuName = "Game Configs/Input/Action Maps/Items Interactions Panel Action Map")]
    public class ItemsInteractionsPanelActionMap : PlayerInputActionMap
    {
        [SerializeField] private LocalizedString _selectActionName;
        [SerializeField] private LocalizedString _closeMenuActionName;

        public override (string name, InputAction inputAction)[] GetActionInfos()
        {
            throw new System.NotImplementedException();
        }
    }
}
