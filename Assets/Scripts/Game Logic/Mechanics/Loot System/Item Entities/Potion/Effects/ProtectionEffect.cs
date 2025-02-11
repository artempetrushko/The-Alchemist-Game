using Cysharp.Threading.Tasks;
using GameLogic.Player;
using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "Protection Potion Effect", menuName = "Game Configs/Items/Potions/Effect/Protection")]
    public class ProtectionEffect : PotionEffect
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
                player.StateManager.AdjustMeleeDamageMitigationPercentage(_power);
            }
        }

        private async UniTask ApplyAsync(PlayerState player)
        {
            player.StateManager.AdjustMeleeDamageMitigationPercentage(_power);

            var timer = _durationInSeconds;
            while (timer > 0)
            {
                await UniTask.WaitForSeconds(1);
                timer--;
            }

            player.StateManager.AdjustMeleeDamageMitigationPercentage(-_power);
        }
    }
}