using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

namespace Controls
{
    public class RunEndingPanelActionMap : PlayerInputActionMap
    {
        [SerializeField] private LocalizedString _returnToHubActionName;
        [SerializeField] private LocalizedString _exitToMainMenuActionName;

        public override (string name, InputAction inputAction)[] GetActionInfos()
        {
            throw new System.NotImplementedException();
        }
    }
}
