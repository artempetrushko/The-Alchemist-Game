using GameLogic.EnvironmentExploration;
using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class Equipment : Item
    {
        public LimitedItemParameter<int> Endurance;

        public Equipment(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
        {
        }
    }
}