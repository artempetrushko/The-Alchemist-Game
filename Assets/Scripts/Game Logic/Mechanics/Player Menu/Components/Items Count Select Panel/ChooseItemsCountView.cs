using System.Collections.Generic;
using Controls;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
    public class ChooseItemsCountView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text actionDescriptionText;
        [SerializeField]
        private Image itemIcon;
        [SerializeField]
        private TMP_Text itemsCounterText;
        [SerializeField]
        private Slider itemsCounterSlider;
        [SerializeField]
        private TMP_Text itemsMinCountText;
        [SerializeField]
        private TMP_Text itemsMaxCountText;
        [Space, SerializeField]
        private ActionButton actionButtonPrefab;
        [SerializeField]
        private GameObject actionButtonsContainer;

        public void StartItemsCountChoosing(Sprite itemIcon, int minItemsCount, int maxItemsCount, string actionDescription, List<(DetailedControlTip controlTip, UnityAction buttonAction)> actionButtonsParams)
        {
            actionDescriptionText.text = actionDescription;
            this.itemIcon.sprite = itemIcon;
            itemsCounterSlider.minValue = minItemsCount;
            itemsCounterSlider.maxValue = maxItemsCount;
            itemsMinCountText.text = minItemsCount.ToString();
            itemsMaxCountText.text = maxItemsCount.ToString();
            SetItemsCounterText(minItemsCount);
            GenerateActionButtons(actionButtonsParams);
        }

        public void SetItemsCounterText(int itemsCount) => itemsCounterText.text = itemsCount.ToString();

        public void SetSliderValueChangedAction(UnityAction<float> valueChangedAction) => itemsCounterSlider.onValueChanged.AddListener(valueChangedAction);

        private void GenerateActionButtons(List<(DetailedControlTip controlTip, UnityAction buttonAction)> actionButtonsParams)
        {
            for (var i = 0; i < actionButtonsParams.Count; i++)
            {
                var actionButton = Instantiate(actionButtonPrefab, actionButtonsContainer.transform);
                var actionOrderNumber = i;
                actionButton.SetInfo(actionButtonsParams[actionOrderNumber].controlTip, actionButtonsParams[actionOrderNumber].buttonAction);
            }
        }
    }
}