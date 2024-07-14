using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace UI.Hud
{
    public class HudController
    {
        private HUDView _hudView;

        private float _titleAppearanceTime = 1f;
        private float _titleDisappearingTime = 0.5f;
        private float _titleShowingTime = 5f;
        private float _questDescriptionShowingTime = 5f;

        public HudController(HUDView hudView)
        {
            _hudView = hudView;
        }

        public async UniTask ShowLocationName_COR(string locationName)
        {
            await _hudView.ShowLocationName_COR(locationName);
        }

        public async UniTask HideStartBlackScreen_COR()
        {
            await _hudView.HideStartBlackScreen_COR();
        }

        public async UniTask ShowLocationNameAsync(string locationTitle)
        {
            _hudView.LocationTitleView.SetLocationTitleText("");
            _hudView.LocationTitleView.SetTitleShadowEnable(true);

            var textAppearanceLatency = _titleAppearanceTime / locationTitle.Length;
            for (var i = 0; i < locationTitle.Length; i++)
            {
                _hudView.LocationTitleView.AddLocationTitleSymbol(locationTitle[i]);
                await UniTask.WaitForSeconds(textAppearanceLatency);
            }

            await UniTask.WaitForSeconds(_titleShowingTime);

            var textDisappearingLatency = _titleDisappearingTime / locationTitle.Length;
            for (var i = 0; i < locationTitle.Length; i++)
            {
                _hudView.LocationTitleView.RemoveLocationTitleLastSymbol();
                await UniTask.WaitForSeconds(textDisappearingLatency);
            }

            _hudView.LocationTitleView.SetTitleShadowEnable(false);
        }

        public async UniTaskVoid SetQuestDescription(string questDescription)
        {
            _hudView.QuestProgressView.SetQuestDescriptionText(questDescription);

            await ShowQuestDescriptionAsync();
        }

        public async UniTask ShowQuestDescriptionAsync()
        {
            _hudView.QuestProgressView.SetActive(true);

            var tweenSequence = DOTween.Sequence();
            tweenSequence
                .Append(_hudView.QuestProgressView.CanvasGroup.DOFade(1f, 1f))
                .AppendInterval(_questDescriptionShowingTime)
                .Append(_hudView.QuestProgressView.CanvasGroup.DOFade(0f, 1f));
            tweenSequence.Play();
            await tweenSequence.AsyncWaitForCompletion();

            _hudView.QuestProgressView.SetActive(false);
        }
    }
}