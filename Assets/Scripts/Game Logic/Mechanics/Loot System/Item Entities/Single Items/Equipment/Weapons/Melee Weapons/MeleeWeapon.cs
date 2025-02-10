using GameLogic.EnvironmentExploration;
using UnityEngine;

namespace GameLogic.LootSystem
{
	public class MeleeWeapon : Weapon
	{
        public ItemParameter<float> BlockingEfficiency { get; set; }

        public MeleeWeapon(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
        {
        }
    }
}