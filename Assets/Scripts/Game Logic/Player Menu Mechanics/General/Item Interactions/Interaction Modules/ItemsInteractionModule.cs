using System;

public abstract class ItemsInteractionModule
{
    public event Action InteractionExecuted;

    protected ItemSlot startItemSlot;

    public abstract void StartInteraction(ItemSlot selectedItemSlot);

    protected void OnInteractionExecuted() => InteractionExecuted?.Invoke();
}