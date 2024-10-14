using System.Collections.Generic;
using GameLogic.EnvironmentExploration;
using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class EquipmentState : ItemState
    {
        public LimitedItemParameter<int> Endurance;
        public ItemParameter<int> MaxRuneSize;
        public ItemParameter<int> PoweredEnergyCount;
        public ItemParameter<int> EnergyCapacity;
        public ItemParameter<List<AspectState>> ContainedAspects;

        public RuneState ImposedRune { get; set; }

        public EquipmentState(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
        {
        }
    }
}