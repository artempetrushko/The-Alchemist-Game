using GameLogic.EnvironmentExploration;
using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class StackableItem : Item
    {
        public LimitedItemParameter<int> Count;

        public int TotalContainedEnergyCount => ContainedEnergyCount.Value * Count.Value;

        public StackableItem(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
        {
        }
    }
}