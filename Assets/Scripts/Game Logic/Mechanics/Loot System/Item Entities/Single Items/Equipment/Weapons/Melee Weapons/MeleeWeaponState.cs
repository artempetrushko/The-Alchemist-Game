using GameLogic.EnvironmentExploration;
using UnityEngine;

namespace GameLogic.LootSystem
{
	public class MeleeWeaponState : WeaponState
	{
        public ItemParameter<float> BlockingEfficiency { get; set; }

        public MeleeWeaponState(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
        {
        }
    }
}