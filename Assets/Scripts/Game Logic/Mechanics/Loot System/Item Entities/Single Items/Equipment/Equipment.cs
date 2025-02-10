using System.Collections.Generic;
using GameLogic.EnvironmentExploration;
using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class Equipment : Item
    {
        public LimitedItemParameter<int> Endurance;
        public ItemParameter<int> MaxRuneSize;
        public ItemParameter<int> PoweredEnergyCount;
        public ItemParameter<int> EnergyCapacity;
        public ItemParameter<List<Aspect>> ContainedAspects;

        public Equipment(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
        {
        }
    }
}