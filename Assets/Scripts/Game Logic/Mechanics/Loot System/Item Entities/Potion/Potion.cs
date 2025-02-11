using GameLogic.EnvironmentExploration;
using GameLogic.Player;
using UnityEngine;

namespace GameLogic.LootSystem
{
    public class Potion : StackableItem, IApplicableItem
    {
        public Potion(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
        {
        }

        public void Apply(PlayerState player)
        {
            throw new System.NotImplementedException();
        }
    }
}