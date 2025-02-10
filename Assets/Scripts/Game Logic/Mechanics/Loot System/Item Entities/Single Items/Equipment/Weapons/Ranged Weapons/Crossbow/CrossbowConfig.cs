using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "New Crossbow", menuName = "Game Configs/Items/Equipment/Weapon/Ranged Weapon/Crossbow")]
    public class CrossbowConfig : RangedWeaponConfig
    {
        public override Item CreateItem() => new Crossbow(Id, Icon, PhysicalRepresentation);
    }
}