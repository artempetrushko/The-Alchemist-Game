using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

namespace Controls
{
    public class PlayerMenuCraftSectionCraftingItemTemplateActionMap : PlayerInputActionMap
    {
        [SerializeField] private LocalizedString _selectSlotActionName;
        [SerializeField] private LocalizedString _clearSlotActionName;
        [SerializeField] private LocalizedString _createItemActionName;
        [SerializeField] private LocalizedString _returnToEnergyCellsActionName;

        public override (string name, InputAction inputAction)[] GetActionInfos()
        {
            throw new System.NotImplementedException();
        }
    }
}
