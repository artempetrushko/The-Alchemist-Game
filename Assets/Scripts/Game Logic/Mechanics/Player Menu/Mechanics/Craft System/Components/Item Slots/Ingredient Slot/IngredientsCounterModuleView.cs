using GameLogic.LootSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
	public class IngredientsCounterModuleView : ItemSlotModule
	{
        [SerializeField] private TMP_Text _itemsCounter;
		[SerializeField] private Image _progressBarFillingArea;
        [SerializeField] private Color _progressBarNormalColor;
        [SerializeField] private Color _progressBarFilledColor;

        public override void UpdateDisplayedInfo(Item item)
        {
            throw new System.NotImplementedException();
        }

        public void SetProgressBarFillingAreaColor(Color color) => _progressBarFillingArea.color = color;

		public void SetProgressBarFillingAreaFillAmount(float fillAmount) => _progressBarFillingArea.fillAmount = fillAmount;
    }
}