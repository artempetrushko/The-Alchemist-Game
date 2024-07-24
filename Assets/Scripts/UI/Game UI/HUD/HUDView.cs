using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private LocationTitleView _locationTitleView;
        [SerializeField] private QuestProgressView _questProgressView;
        [SerializeField] private Image _startBlackScreen;

        public LocationTitleView LocationTitleView => _locationTitleView;
        public QuestProgressView QuestProgressView => _questProgressView;
        public Image StartBlackScreen => _startBlackScreen;

        public void SetStartBlackScreenActive(bool isActive) => _startBlackScreen.gameObject.SetActive(isActive);
    }
}