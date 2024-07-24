using Controls;
using Cysharp.Threading.Tasks;

public class PotionApplyer
{
    private ABC_StateManager playerStateManager;
    private InventoryManager inventoryManager;
    private EquipmentManager playerSetItems;

    private InputManager _inputManager;

    public PotionApplyer()
    {
        playerStateManager = GetComponent<ABC_StateManager>();
        //inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void ApplyCurrentPotion()
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
                ApplyEffectAsync(currentPotion, potionEffectApplyingActions)).Forget();
            }
            else
            {
                potionEffectApplyingActions.InstantApplyingAction.Invoke(currentPotion);
            }
        }*/
    }

    private async UniTask ApplyEffectAsync(PotionState currentPotion, EffectApplyingActions effectApplyingActions)
    {
        effectApplyingActions.TimerStartAction?.Invoke(currentPotion);
        var effectRemainingTime = (currentPotion.BaseParams as PotionData).EffectDurationInSeconds;
        while (effectRemainingTime > 0)
        {
            effectApplyingActions.TimerProceedAction?.Invoke(currentPotion);
            effectRemainingTime--;
            await UniTask.WaitForSeconds(1f);
        }
        effectApplyingActions.TimerEndAction?.Invoke(currentPotion);
    }
}
