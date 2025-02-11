using GameLogic.EnvironmentExploration;
using UnityEngine;

namespace GameLogic.LootSystem
{
    public class Clothes : Equipment
    {
        public ItemParameter<int> Defence;

        public Clothes(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
        {
        }
    }
}