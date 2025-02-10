using GameLogic.LootSystem;
using TMPro;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
	public class EnergyCounterModuleView : ItemSlotModule
	{
		[SerializeField] private TMP_Text _energyCounter;

        public override void UpdateDisplayedInfo(Item item)
        {
            _energyCounter.text = item switch
            {
                StackableItem stackableItem => stackableItem.TotalContainedEnergyCount.ToString(),
                _ => item.ContainedEnergyCount.ToString()
            };
        }
    }
}