using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

namespace Controls
{
    public class PlayerMenuItemsCountSelectPanelActionMap : PlayerInputActionMap
    {
        [SerializeField] private LocalizedString _selectActionName;
        [SerializeField] private LocalizedString _selectAllActionName;
        [SerializeField] private LocalizedString _cancelActionName;

        public override (string name, InputAction inputAction)[] GetActionInfos()
        {
            throw new System.NotImplementedException();
        }
    }
}
