using UnityEngine;

namespace GameLogic.LootSystem
{
	[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Game Configs/Items/Equipment/Weapon/Melee Weapon")]
	public class MeleeWeaponData : WeaponData
	{
		[SerializeField] private float blockingEfficiency;

		public float BaseBlockingEfficiency => blockingEfficiency;

		public override ItemState CreateItem() => new MeleeWeaponState(Id, Icon, PhysicalRepresentation);
	}
}