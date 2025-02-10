using Cysharp.Threading.Tasks;

namespace GameLogic.HUD
{
    public class HUDManager
    {
        private HUDView _hudView;

        public HUDManager(HUDView hudView)
        {
            _hudView = hudView;
        }

        public async UniTask ShowLocationNameAsync(string locationName)
        {
            await _hudView.ShowLocationNameAsync(locationName);
        }

        public async UniTask HideStartBlackScreenAsync()
        {
            await _hudView.HideStartBlackScreenAsync();
        }
    }
}