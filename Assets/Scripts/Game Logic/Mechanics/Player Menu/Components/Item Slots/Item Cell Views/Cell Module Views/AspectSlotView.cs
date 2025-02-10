using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.PlayerMenu
{
    public class AspectSlotView : MonoBehaviour
    {
        [SerializeField]
        private Image border;
        [SerializeField]
        private Image innerArea;

        public void SetAppearance(Sprite innerAreaIcon, float innerAreaFillAmount)
        {
            innerArea.sprite = innerAreaIcon;
            innerArea.fillAmount = innerAreaFillAmount;
        }
    }
}