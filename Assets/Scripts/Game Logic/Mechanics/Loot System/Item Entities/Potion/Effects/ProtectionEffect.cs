using Cysharp.Threading.Tasks;
using EventBus;
using UnityEngine;
using Zenject;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "Protection Potion Effect", menuName = "Game Configs/Items/Potions/Effect/Protection")]
    public class ProtectionEffect : PotionEffect
    {
        [SerializeField] private int _power;
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
            else
            {
                _signalBus.Fire(new PlayerStateChangedSignal((state) => state.AdjustMeleeDamageMitigationPercentage(_power)));
            }
        }

        private async UniTask ApplyAsync()
        {
            _signalBus.Fire(new PlayerStateChangedSignal((state) => state.AdjustMeleeDamageMitigationPercentage(_power)));

            var timer = _durationInSeconds;
            while (timer > 0)
            {
                await UniTask.WaitForSeconds(1);
                timer--;
            }

            _signalBus.Fire(new PlayerStateChangedSignal((state) => state.AdjustMeleeDamageMitigationPercentage(-_power)));
        }
    }
}