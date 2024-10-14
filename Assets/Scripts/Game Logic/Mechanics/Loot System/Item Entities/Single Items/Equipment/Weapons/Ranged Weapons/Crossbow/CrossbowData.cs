using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "New Crossbow", menuName = "Game Configs/Items/Equipment/Weapon/Ranged Weapon/Crossbow")]
    public class CrossbowData : RangedWeaponData
    {
        public override ItemState CreateItem() => new CrossbowState(Id, Icon, PhysicalRepresentation);
    }
}