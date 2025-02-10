using TMPro;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    public class CraftingProcessStateView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text energyCounterLabel;
        [SerializeField]
        private TMP_Text energyCounter;
        [SerializeField]
        private TMP_Text craftingAvailabilityLabel;
        [Space, SerializeField]
        private Color craftingAvailabilityColor;
        [SerializeField]
        private Color craftingUnavailabilityColor;

        public void SetExtractedEnergyCountInfo(string energyCounterLabelText, int currentEnergyCount, int requiredEnergyCount)
        {
            energyCounterLabel.text = energyCounterLabelText;
            energyCounter.text = $"{currentEnergyCount}/{requiredEnergyCount}";
        }

        public void SetCreationAvailabilityState(bool isCraftingAvailable, string craftingAvailabilityText)
        {
            craftingAvailabilityLabel.text = craftingAvailabilityText;
            craftingAvailabilityLabel.color = isCraftingAvailable ? craftingAvailabilityColor : craftingUnavailabilityColor;
        }

        private void OnEnable()
        {
            energyCounter.text = energyCounterLabel.text = craftingAvailabilityLabel.text = "";
        }
    }
}