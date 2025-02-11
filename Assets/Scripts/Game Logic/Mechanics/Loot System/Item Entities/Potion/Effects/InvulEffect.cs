using Cysharp.Threading.Tasks;
using GameLogic.Player;
using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "Invul Potion Effect", menuName = "Game Configs/Items/Potions/Effect/Invul")]
    public class InvulEffect : PotionEffect
    {
        [SerializeField] private int _durationInSeconds;

        public override void Apply(PlayerState player)
        {
            if (_durationInSeconds > 0)
            {
                ApplyAsync(player).Forget();
            }
        }

        private async UniTask ApplyAsync(PlayerState player)
        {
            player.StateManager.ToggleEffectProtection(true);

            var timer = _durationInSeconds;
            while (timer > 0)
            {
                await UniTask.WaitForSeconds(1);
                timer--;
            }

            player.StateManager.ToggleEffectProtection(false);
        }
    }
}