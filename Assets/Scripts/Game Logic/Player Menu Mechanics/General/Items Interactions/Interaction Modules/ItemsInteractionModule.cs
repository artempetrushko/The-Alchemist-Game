using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemInteraction
{
    BindQuickAccess,
    Join,
    Split,
    Drop,
    Equip,
    TakeOff,
    ChangeHand,
    ExtractEnergy,
    UseAsIngredient
}

public abstract class ItemsInteractionModule : MonoBehaviour
{
    public event Action InteractionExecuted;

    protected ItemSlot startItemSlot;

    [SerializeField]
    private ItemInteraction interaction;
    [SerializeField]
    private string displayedName;
    [SerializeField]
    private Sprite icon;

    public ItemInteraction Interaction => interaction;
    public string DisplayedName => displayedName;
    public Sprite Icon => icon;

    public abstract void StartInteraction(ItemSlot selectedItemSlot);

    protected void OnInteractionExecuted() => InteractionExecuted?.Invoke();
}