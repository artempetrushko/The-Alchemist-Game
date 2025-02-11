using UnityEngine;

namespace GameLogic.LootSystem
{
	[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Game Configs/Items/Equipment/Weapon/Melee Weapon")]
	public class MeleeWeaponConfig : WeaponConfig
	{
		[SerializeField] private float _blockingEfficiency;

		public float BaseBlockingEfficiency => _blockingEfficiency;

		public override Item CreateItem() => new MeleeWeapon(Id, Icon, PhysicalRepresentation);
	}
}