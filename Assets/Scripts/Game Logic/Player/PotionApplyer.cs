using System.Collections;
using GameLogic.LootSystem;
using GameLogic.PlayerMenu;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameLogic.Player
{
    public class PotionApplyer : MonoBehaviour
    {
        private ABC_StateManager playerStateManager;
        private InventoryManager inventoryManager;
        private EquipmentManager playerSetItems;

        public void ApplyCurrentPotion(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                ApplyCurrentPotion();
            }
        }

        public void ApplyCurrentPotion()
        {
            /*var currentPotion = playerSetItems.SelectedQuickAccessItem as PotionState;
            if (currentPotion != null)
            {
                currentPotion.ItemsCount--;
                if (currentPotion.ItemsCount == 0)
                {
                    //inventoryManager.RemoveItemState(currentPotion);
                }

                var potionEffectApplyingActions = currentPotion.GetEffectApplyingActions(playerStateManager);
                if (currentPotion.EffectDuration > 0)
                {
                    StartCoroutine(ApplyEffect_COR(currentPotion, potionEffectApplyingActions));
                }
                else
                {
                    potionEffectApplyingActions.InstantApplyingAction.Invoke(currentPotion);
                }
            }*/
        }

        private IEnumerator ApplyEffect_COR(PotionState currentPotion, EffectApplyingActions effectApplyingActions)
        {
            effectApplyingActions.TimerStartAction?.Invoke(currentPotion);
            var effectRemainingTime = (currentPotion.BaseParams as PotionData).EffectDurationInSeconds;
            while (effectRemainingTime > 0)
            {
                effectApplyingActions.TimerProceedAction?.Invoke(currentPotion);
                effectRemainingTime--;
                yield return new WaitForSeconds(1f);
            }
            effectApplyingActions.TimerEndAction?.Invoke(currentPotion);
        }

        private void Start()
        {
            playerStateManager = GetComponent<ABC_StateManager>();
            inventoryManager = FindObjectOfType<InventoryManager>();
        }
    }
}