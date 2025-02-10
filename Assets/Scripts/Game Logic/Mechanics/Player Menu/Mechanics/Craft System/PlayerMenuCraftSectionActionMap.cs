using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

namespace Controls
{
    public class PlayerMenuCraftSectionActionMap : PlayerInputActionMap
    {
        [SerializeField] private LocalizedString _selectActionName;
        [SerializeField] private LocalizedString _createItemActionName;

        public override (string name, InputAction inputAction)[] GetActionInfos()
        {
            throw new System.NotImplementedException();
        }
    }
}
