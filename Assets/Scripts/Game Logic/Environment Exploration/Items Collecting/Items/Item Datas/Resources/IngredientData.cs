using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Game Entities/Items/Resources/Ingredient", order = 51)]
public class IngredientData : ResourceData
{
    [Header("Параметры ингредиента")]
    [SerializeField]
    protected List<ItemEffect> potionContainedEffects = new();
    [SerializeField]
    protected List<ItemEffect> bombContainedEffects = new();

    public List<ItemEffect> PotionContainedEffects => potionContainedEffects;
    public List<ItemEffect> BombContainedEffects => bombContainedEffects;

    public override ItemState GetItemState() => new IngredientState(this, BaseCount);
}
