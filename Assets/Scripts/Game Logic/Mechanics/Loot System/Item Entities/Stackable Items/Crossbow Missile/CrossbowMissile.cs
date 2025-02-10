using GameLogic.EnvironmentExploration;
using GameLogic.LootSystem;
using UnityEngine;

public class CrossbowMissile : StackableItem
{
    public ItemParameter<float> PenetratingPowerCoefficient;

    public CrossbowMissile(string id, Sprite icon, PickableItem physicalRepresentation) : base(id, icon, physicalRepresentation)
    {
    }
}
