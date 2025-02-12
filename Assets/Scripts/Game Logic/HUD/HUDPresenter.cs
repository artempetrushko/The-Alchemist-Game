using Cysharp.Threading.Tasks;

namespace GameLogic
{
    public class HUDPresenter
    {
        private float TITLE_APPEARANCE_TIME_IN_SECONDS = 1f;
        private float TITLE_HIDING_TIME_IN_SECONDS = 0.5f;
        private float TITLE_SHOWING_TIME_IN_SECONDS = 5f;

        private HUDView _hudView;

        public HUDPresenter(HUDView hudView)
        {
            _hudView = hudView;
        }

        public async UniTask ShowLocationNameAsync(string locationTitle)
        {
            await _hudView.LocationTitleView.ShowLocationTitleAsync(locationTitle, TITLE_APPEARANCE_TIME_IN_SECONDS);
            await UniTask.WaitForSeconds(TITLE_SHOWING_TIME_IN_SECONDS);
            await _hudView.LocationTitleView.HideLocationTitleAsync(TITLE_HIDING_TIME_IN_SECONDS);
        }

        public async UniTask HideStartBlackScreenAsync() => await _hudView.PlayHideBlackScreenAnimationAsync(2f);
    }
}