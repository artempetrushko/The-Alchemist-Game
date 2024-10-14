using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "New Resource", menuName = "Game Configs/Items/Crossbow Missile")]
    public class CrossbowMissileData : StackableItemConfig
    {

        public override ItemState CreateItem() => new CrossbowMissileState(Id, Icon, PhysicalRepresentation);
    }
}