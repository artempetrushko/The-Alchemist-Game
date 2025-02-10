using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controls
{
    public class ControlTipView : MonoBehaviour
    {
        [SerializeField]
        protected TMP_Text keyName;
        [SerializeField]
        protected GameObject gamepadKeyIconContainer;
        [SerializeField]
        protected Image keyIconContainer;

        public void SetInfo(ControlTip actionTip)
        {
            ToggleViewElements(actionTip.KeyIcon != null);
            if (actionTip.KeyIcon != null)
            {
                gamepadKeyIconContainer.GetComponentInChildren<Image>().sprite = actionTip.KeyIcon;
            }
            else
            {
                keyName.text = actionTip.KeyName;
            }
        }

        protected void ToggleViewElements(bool isKeyIconAvailable)
        {
            keyIconContainer.gameObject.SetActive(!isKeyIconAvailable);
            gamepadKeyIconContainer.SetActive(isKeyIconAvailable);
        }
    }
}