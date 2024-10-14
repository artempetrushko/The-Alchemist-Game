using GameLogic.EnvironmentExploration;
using GameLogic.Player;
using UnityEngine;

namespace GameLogic.LootSystem
{
    public class PotionState : StackableItemState, IApplicableItem
    {
        public PotionState(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
        {
        }

        public void Apply(PlayerState player)
        {
            throw new System.NotImplementedException();
        }
    }
}