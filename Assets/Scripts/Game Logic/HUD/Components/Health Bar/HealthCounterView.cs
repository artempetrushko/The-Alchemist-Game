using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class HealthCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthCounter;
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _healthBarFillingArea;

        public void SetHealthCounterText(string text) => _healthCounter.text = text;

        public void SetHealthBarSprite(Sprite sprite) => _healthBar.sprite = sprite;

        public void SetHealthBarFillingAreaSprite(Sprite sprite) => _healthBarFillingArea.sprite = sprite;

        public void SetHealthBarFillingAreaFillAmount(float fillAmount) => _healthBarFillingArea.fillAmount = fillAmount;
    }
}