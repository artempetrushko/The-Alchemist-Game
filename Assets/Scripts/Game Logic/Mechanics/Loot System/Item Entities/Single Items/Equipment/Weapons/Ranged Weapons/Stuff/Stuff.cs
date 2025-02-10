using GameLogic.EnvironmentExploration;
using UnityEngine;

namespace GameLogic.LootSystem
{
	public class Stuff : RangedWeapon
	{
		public ItemParameter<StuffAttackType> AttackType;

        public Stuff(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
        {
        }
    }
}