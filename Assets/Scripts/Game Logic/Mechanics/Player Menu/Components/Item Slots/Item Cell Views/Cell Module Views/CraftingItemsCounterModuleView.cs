using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
    public class CraftingItemsCounterModuleView : MonoBehaviour
    {
        [SerializeField]
        private Image counterProgressBar;
        [SerializeField]
        private TMP_Text counterText;
        [Space, SerializeField]
        private Color normalProgressBarColor;
        [SerializeField]
        private Color filledProgressBarColor;

        public void UpdateItemsCounter(int currentItemsCount, int? requiredItemsCount)
        {
            counterText.text = string.Format("{0}/{1}", currentItemsCount, requiredItemsCount != null ? requiredItemsCount : "?");
            counterProgressBar.fillAmount = requiredItemsCount != null
                ? Mathf.Clamp01(((float)currentItemsCount / requiredItemsCount).Value)
                : 0;
            counterProgressBar.color = counterProgressBar.fillAmount == 1 ? filledProgressBarColor : normalProgressBarColor;
        }
    }
}