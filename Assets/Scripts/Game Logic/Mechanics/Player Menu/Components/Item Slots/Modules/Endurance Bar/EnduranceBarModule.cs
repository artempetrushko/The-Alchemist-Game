using GameLogic.LootSystem;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
    public class EnduranceBarModule : ItemSlotModule
    {
        [SerializeField] private Image _fillingArea;
        [SerializeField] private EnduranceBarConfig _enduranceBarConfig;

        public override void UpdateDisplayedInfo(Item item)
        {
            if (item is Equipment equipment)
            {
                SetVisible(true);
                _fillingArea.fillAmount = (float)equipment.Endurance.Value / equipment.Endurance.MaxValue;
                _fillingArea.color = GetCurrentState(_fillingArea.fillAmount * 100).Color;
            }
            else
            {
                SetVisible(false);
            }
        }

        private EnduranceBarState GetCurrentState(float endurancePercentage)
        {
            foreach (var state in _enduranceBarConfig.States)
            {
                if (state.MinEndurancePercentage <= endurancePercentage && endurancePercentage <= state.MaxEndurancePercentage)
                {
                    return state;
                }
            }
            return _enduranceBarConfig.States[0];
        }
    }
}