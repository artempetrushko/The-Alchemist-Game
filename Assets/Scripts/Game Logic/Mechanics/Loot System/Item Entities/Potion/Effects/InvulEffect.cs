using Cysharp.Threading.Tasks;
using EventBus;
using UnityEngine;
using Zenject;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "Invul Potion Effect", menuName = "Game Configs/Items/Potions/Effect/Invul")]
    public class InvulEffect : PotionEffect
    {
        [SerializeField] private int _durationInSeconds;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public override void Apply()
        {
            if (_durationInSeconds > 0)
            {
                ApplyAsync().Forget();
            }
        }

        private async UniTask ApplyAsync()
        {
            _signalBus.Fire(new PlayerStateChangedSignal((state) => state.ToggleEffectProtection(true)));

            var timer = _durationInSeconds;
            while (timer > 0)
            {
                await UniTask.WaitForSeconds(1);
                timer--;
            }

            _signalBus.Fire(new PlayerStateChangedSignal((state) => state.ToggleEffectProtection(false)));
        }
    }
}