using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

namespace Controls
{
    public class PlayerMenuItemSlotActionsMenuActionMap : PlayerInputActionMap
    {
        [SerializeField] private LocalizedString _selectActionName;
        [SerializeField] private LocalizedString _closeMenuActionName;

        public override (string name, InputAction inputAction)[] GetActionInfos()
        {
            throw new System.NotImplementedException();
        }
    }
}
