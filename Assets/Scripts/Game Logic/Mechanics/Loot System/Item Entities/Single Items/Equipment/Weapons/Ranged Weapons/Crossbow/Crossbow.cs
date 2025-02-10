using GameLogic.EnvironmentExploration;
using GameLogic.LootSystem;
using UnityEngine;

public class Crossbow : RangedWeapon
{
    public Crossbow(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
    {
    }
}
