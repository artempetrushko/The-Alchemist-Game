using Cysharp.Threading.Tasks;
using GameLogic.Player;
using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "Heal Potion Effect", menuName = "Game Configs/Items/Potions/Effect/Heal")]
    public class HealEffect : PotionEffect
    {
        [SerializeField] private int _power;
        [SerializeField] private int _durationInSeconds;

        public override void Apply(PlayerState player)
        {
            if (_durationInSeconds > 0)
            {
                ApplyAsync(player).Forget();
            }
            else
            {
                player.StateManager.AdjustHealth(_power);
            }
        }

        private async UniTask ApplyAsync(PlayerState player)
        {
            var timer = _durationInSeconds;
            while (timer > 0)
            {
                player.StateManager.AdjustHealth(_power);

                await UniTask.WaitForSeconds(1);
                timer--;
            }
        }
    }
}