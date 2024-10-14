using GameLogic.EnvironmentExploration;
using UnityEngine;

namespace GameLogic.LootSystem
{
	public class StuffState : RangedWeaponState
	{
		public ItemParameter<StuffAttackType> AttackType;

        public StuffState(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
        {
        }
    }
}