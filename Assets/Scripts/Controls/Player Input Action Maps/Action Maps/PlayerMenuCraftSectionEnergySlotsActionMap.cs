using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

namespace Controls
{
    public class PlayerMenuCraftSectionEnergySlotsActionMap : PlayerInputActionMap
    {
        [SerializeField] private LocalizedString _selectSlotActionMap;
        [SerializeField] private LocalizedString _goToCraftingItemTemplateActionName;
        [SerializeField] private LocalizedString _createItemActionName;

        public override (string name, InputAction inputAction)[] GetActionInfos()
        {
            throw new System.NotImplementedException();
        }
    }
}
