using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Game Configs/Items/Crossbow Missile")]
    public class CrossbowMissileConfig : StackableItemConfig
    {

        public override Item CreateItem() => new CrossbowMissile(Id, Icon, PhysicalRepresentation);
    }
}