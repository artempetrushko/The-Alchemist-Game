using System;
using UnityEngine;

namespace GameLogic.LootSystem
{

    public class EffectApplyingActions
    {
        public Action<PotionState> TimerStartAction;
        public Action<PotionState> TimerProceedAction;
        public Action<PotionState> TimerEndAction;
        public Action<PotionState> InstantApplyingAction;
    }

    public class PotionEffects : MonoBehaviour
    {
        public static EffectApplyingActions GetEffectApplyingActions(PotionState potion, ABC_StateManager effectReceiver)
        {
            return (potion.BaseParams as PotionData).Effect switch
            {
                PotionEffect.Heal => new EffectApplyingActions()
                {
                    TimerStartAction = null,
                    TimerProceedAction = (potion) => effectReceiver.AdjustHealth((potion.BaseParams as PotionData).EffectPower),
                    TimerEndAction = null,
                    InstantApplyingAction = (potion.BaseParams as PotionData).EffectDurationInSeconds == 0
                        ? (potion) => effectReceiver.AdjustHealth((potion.BaseParams as PotionData).EffectDurationInSeconds)
                        : null,
                },
                PotionEffect.Protection => new EffectApplyingActions()
                {
                    TimerStartAction = (potion) => effectReceiver.AdjustMeleeDamageMitigationPercentage((potion.BaseParams as PotionData).EffectPower),
                    TimerProceedAction = null,
                    TimerEndAction = (potion) => effectReceiver.AdjustMeleeDamageMitigationPercentage(-(potion.BaseParams as PotionData).EffectPower),
                    InstantApplyingAction = (potion.BaseParams as PotionData).EffectDurationInSeconds == 0
                        ? (potion) => effectReceiver.AdjustMeleeDamageMitigationPercentage((potion.BaseParams as PotionData).EffectPower)
                        : null,
                },
                PotionEffect.Invul => new EffectApplyingActions()
                {
                    TimerStartAction = (potion) => effectReceiver.ToggleEffectProtection(true),
                    TimerProceedAction = null,
                    TimerEndAction = (potion) => effectReceiver.ToggleEffectProtection(false),
                    InstantApplyingAction = (potion.BaseParams as PotionData).EffectDurationInSeconds == 0
                        ? (potion) => effectReceiver.ToggleEffectProtection(true)
                        : null,
                }
            };
        }
    }
}
