using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentData : SingleItemData
{
    [Header("Параметры снаряжения")]
    [SerializeField]
    protected Sprite bigIcon;
    [SerializeField]
    protected int endurance;
    [SerializeField]
    protected int maxRuneSize;
    [SerializeField]
    protected int poweredEnergyCount;
    [SerializeField]
    protected int energyCapacity;
    [SerializeField]
    protected List<RequiredComponentPart> componentParts = new();

    public Sprite BigIcon => bigIcon;
    public int BaseEndurance => endurance;
    public int BaseMaxRuneSize => maxRuneSize;
    public int BasePoweredEnergyCount => poweredEnergyCount;
    public int BaseEnergyCapacity => energyCapacity;
    public List<RequiredComponentPart> BaseComponentParts => componentParts;
}
