using TMPro;
using UnityEngine;

namespace UI.Hud
{
    public class QuestProgressView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _questDescriptionText;

        public CanvasGroup CanvasGroup => _canvasGroup;

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetQuestDescriptionText(string text)
        {
            _questDescriptionText.text = text;
        }
    }
}