using GameLogic.EnvironmentExploration;
using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class StackableItemState : ItemState
    {
        public LimitedItemParameter<int> Count;

        public int TotalContainedEnergyCount => ContainedEnergyCount.Value * Count.Value;

        public StackableItemState(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
        {
        }
    }
}