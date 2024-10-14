using GameLogic.LootSystem;
using TMPro;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
	public class EnergyCounterModuleView : ItemSlotModule
	{
		[SerializeField] private TMP_Text _energyCounter;

        public override void UpdateDisplayedInfo(ItemState item)
        {
            _energyCounter.text = item switch
            {
                StackableItemState stackableItem => stackableItem.TotalContainedEnergyCount.ToString(),
                _ => item.ContainedEnergyCount.ToString()
            };
        }
    }
}