using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionEffect
{ 
    Heal,
    Protection,
    Invul
}

[CreateAssetMenu(fileName = "New Potion", menuName = "Game Entities/Items/Potion", order = 51)]
public class PotionData : StackableItemData
{
    [Header("Параметры зелий")]
    [SerializeField]
    private PotionEffect effect;
    [SerializeField]
    private int effectPower;
    [SerializeField]
    private int effectDurationInSeconds;

    public PotionEffect Effect => effect;
    public int EffectPower => effectPower;
    public int EffectDurationInSeconds => effectDurationInSeconds;

    public override ItemState GetItemState() => new PotionState(this);
}
