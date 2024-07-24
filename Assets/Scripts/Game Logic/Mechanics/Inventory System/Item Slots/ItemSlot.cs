using GameLogic;
using System;
using System.Collections.Generic;

public abstract class ItemSlot
{
    public event Action ItemStateChanged;

    protected ItemCellView cellView;
    protected ItemCellView linkedHUDCellView;
    protected Func<List<ItemInteractionType>> getItemInteractionsFunc;

    public abstract ItemState BaseItemState { get; } 
    public ItemCellView CellView
    {
        get => cellView;
        set
        {
            cellView = value;
            if (cellView != null)
            {
                cellView.LinkedItemSlot = this;
            }
            UpdateCellView(cellView);
        }
    }
    public ItemCellView LinkedHUDCellView
    {
        get => linkedHUDCellView;
        set
        {
            linkedHUDCellView = value;
            if (linkedHUDCellView != null)
            {
                linkedHUDCellView.LinkedItemSlot = this;
            }
            UpdateHUDCellView(linkedHUDCellView);
        }
    }

    public abstract bool TryPlaceItem(ItemState item);

    public abstract bool TryPlaceOrSwapItem(ItemSlot previousInventorySlot);

    public abstract List<ItemInteractionType> GetItemInteractions();

    public abstract void ClearItemState();

    protected void OnItemStateChanged() => ItemStateChanged?.Invoke();

    protected void UpdateCellView(ItemCellView cellView)
    {
        if (cellView != null)
        {
            if (BaseItemState != null)
            {
                cellView.PlaceItemView(BaseItemState);
                cellView.EnableAndUpdateCellModules(BaseItemState);
            }
            else
            {
                cellView.DisableCellModules();
            }
        }
    } 

    protected void UpdateHUDCellView(ItemCellView cellView)
    {
        if (cellView != null)
        {
            if (BaseItemState != null)
            {
                cellView.PlaceItemViewCopy(BaseItemState);
                cellView.EnableAndUpdateCellModules(BaseItemState);
            }
            else
            {
                cellView.DisableCellModules();
                cellView.ClearItemView();
            }
        }
    }
}

public abstract class ItemSlot<T> : ItemSlot where T : ItemState
{
    protected T itemState;

    public override ItemState BaseItemState => ItemState;
    public virtual T ItemState
    {
        get => itemState;
        protected set
        {
            if (itemState != null && itemState.LinkedItemSlot == this)
            {
                itemState.LinkedItemSlot = null;
            }         
            itemState = value;
            if (itemState != null)
            {
                itemState.LinkedItemSlot = this;
            }
            UpdateCellView(CellView);
            UpdateHUDCellView(LinkedHUDCellView);
            OnItemStateChanged();
        }
    }

    public override bool TryPlaceItem(ItemState item)
    {
        if (item is T accordingItem)
        {
            ItemState = accordingItem;
            return true;
        }
        return false;
    }

    public override bool TryPlaceOrSwapItem(ItemSlot previousInventorySlot)
    {
        if (previousInventorySlot.BaseItemState is T)
        {
            try
            {
                return TryPlaceOrSwapItem(previousInventorySlot as ItemSlot<T>);
            }
            catch
            {
                return false;
            }
        }
        return false;
    }

    public override void ClearItemState() => ItemState = null;

    protected virtual bool TryPlaceOrSwapItem<P>(ItemSlot<P> previousInventorySlot) where P : ItemState
    {
        PlaceOrSwapItem(previousInventorySlot);
        return true;
    }

    protected void PlaceOrSwapItem<P>(ItemSlot<P> previousInventorySlot) where P : ItemState
    {
        if (previousInventorySlot.ItemState != null)
        {
            SwapItems(previousInventorySlot);
        }
        else
        {
            PlaceItem(previousInventorySlot);
        }
    }

    protected void SwapItems<P>(ItemSlot<P> otherInventorySlot) where P : ItemState
    {
        (ItemState, otherInventorySlot.ItemState) = (otherInventorySlot.ItemState as T, ItemState as P);
    }

    protected void PlaceItem<P>(ItemSlot<P> previousInventorySlot) where P : ItemState
    {
        ItemState = previousInventorySlot.ItemState as T;
        previousInventorySlot.ItemState = null;
    }
}
