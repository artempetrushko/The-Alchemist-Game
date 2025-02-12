using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "New Stuff", menuName = "Game Configs/Items/Equipment/Weapon/Ranged Weapon/Stuff")]
    public class StuffConfig : RangedWeaponConfig
    {
        [SerializeField] private StuffAttackType _attackType;

        public StuffAttackType BaseAttackType => _attackType;

        public override Item CreateItem() => new Stuff(Id, Icon, PhysicalRepresentation);
    }
}