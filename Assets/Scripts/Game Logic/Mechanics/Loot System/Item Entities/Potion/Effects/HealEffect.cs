using Cysharp.Threading.Tasks;
using EventBus;
using UnityEngine;
using Zenject;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "Heal Potion Effect", menuName = "Game Configs/Items/Potions/Effect/Heal")]
    public class HealEffect : PotionEffect
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
                ApplyEffect();
            }
        }

        private async UniTask ApplyAsync()
        {
            var timer = _durationInSeconds;
            while (timer > 0)
            {
                ApplyEffect();

                await UniTask.WaitForSeconds(1);
                timer--;
            }
        }

        private void ApplyEffect() => _signalBus.Fire(new PlayerStateChangedSignal((state) => state.AdjustHealth(_power)));
    }
}