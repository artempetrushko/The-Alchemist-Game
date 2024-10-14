using UnityEngine;

namespace GameLogic.LootSystem
{
	[CreateAssetMenu(fileName = "New Stuff", menuName = "Game Configs/Items/Equipment/Weapon/Ranged Weapon/Stuff")]
	public class StuffData : RangedWeaponData
	{
		[SerializeField] private StuffAttackType attackType;

		public StuffAttackType BaseAttackType => attackType;

		public override ItemState CreateItem() => new StuffState(Id, Icon, PhysicalRepresentation);
	}
}