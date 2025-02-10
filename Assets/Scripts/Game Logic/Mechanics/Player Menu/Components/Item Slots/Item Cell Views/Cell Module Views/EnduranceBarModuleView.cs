using GameLogic.LootSystem;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
    public class EnduranceBarModuleView : ItemCellModuleView
    {
        [SerializeField]
        private Image enduranceBar;
        [SerializeField]
        private Image enduranceBarFillingPart;
        [SerializeField]
        private EnduranceBarData enduranceBarStates;

        public override void SetActive(bool isActive)
        {
            enduranceBar.gameObject.SetActive(isActive);
        }

        public override bool TryEnableWithNewItem(ItemState newItem)
        {
            if (newItem is EquipmentState)
            {
                SetActive(true);
                return true;
            }
            return false;
        }

        public override void UpdateContent(ItemState attachedItem)
        {
            if (attachedItem is EquipmentState equipment)
            {
                enduranceBarFillingPart.fillAmount = (float)equipment.Endurance / equipment.MaxEndurance;
                enduranceBarFillingPart.color = enduranceBarStates.GetEnduranceBarColor(enduranceBarFillingPart.fillAmount * 100);
            }
        }
    }
}