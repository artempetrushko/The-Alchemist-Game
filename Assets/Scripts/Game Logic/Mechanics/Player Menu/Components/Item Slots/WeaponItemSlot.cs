using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponItemSlot : ItemSlot<WeaponState>
{
    public event Action<WeaponState, WeaponHandPosition> WeaponStateChanged;

    [SerializeField]
    private WeaponHandPosition weaponHandPosition;

    public override WeaponState ItemState
    {
        get => base.ItemState;
        set
        {
            base.ItemState = value;
            WeaponStateChanged?.Invoke(base.ItemState, WeaponHandPosition);        
        }
    }
    public WeaponHandPosition WeaponHandPosition => weaponHandPosition;

    public override List<ItemInteraction> GetItemInteractions()
    {
        return new()
        {
            ItemInteraction.ChangeHand,
            ItemInteraction.TakeOff,
            ItemInteraction.Drop
        };
    }
}
