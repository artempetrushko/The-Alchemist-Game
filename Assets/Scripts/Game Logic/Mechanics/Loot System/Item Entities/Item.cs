using System.Collections.Generic;
using GameLogic.EnvironmentExploration;
using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic.LootSystem
{
    public abstract class Item
    {
        public readonly string Id;
        public readonly string InstanceId;
        public readonly Sprite Icon;
        public readonly PickableItem PhysicalRepresentation;

        public LocalizedString Title;
        public LocalizedString Description;
        public ItemParameter<int> ContainedEnergyCount;
        public ItemParameter<int> CastingDamage;

        public Item(string id, Sprite icon, PickableItem physicalRepresentation)
        {
            Id = id;
            InstanceId = GenerateStateId(); 
            Icon = icon;
            PhysicalRepresentation = physicalRepresentation;
        }

        //TODO
        protected string GenerateStateId()
        {
            return null;
        }
    }
}