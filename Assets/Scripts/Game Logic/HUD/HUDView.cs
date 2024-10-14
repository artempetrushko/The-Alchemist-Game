using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private LocationTitleView _locationTitleView;
        [SerializeField] private Image _startBlackScreen;

        public LocationTitleView LocationTitleView => _locationTitleView;

        public async UniTask PlayHideBlackScreenAnimationAsync(float duration)
        {
            _startBlackScreen.gameObject.SetActive(true);
            await _startBlackScreen
                .DOFade(0f, duration)
                .AsyncWaitForCompletion();
            _startBlackScreen.gameObject.SetActive(false);
        }
    }
}