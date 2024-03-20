using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Общие параметры")]
    [SerializeField]
    protected int id;
    [SerializeField]
    protected Sprite icon;
    [SerializeField]
    protected PickableItem physicalRepresentation;
    [SerializeField]
    protected string title;
    [SerializeField, Multiline]
    protected string description;
    [SerializeField]
    protected int castingDamage;
    [SerializeField]
    protected List<AspectData> containedAspects = new();
    [SerializeField]
    protected List<ItemEffect> effects = new();

    public int ID => id;
    public Sprite Icon => icon;
    public PickableItem PhysicalRepresentation => physicalRepresentation;
    public string Title => title;
    public string BaseDescription => description;
    public int BaseCastingDamage => castingDamage;
    public List<AspectData> BaseContainedAspects => containedAspects;
    public List<ItemEffect> BaseEffects => effects;

    public abstract ItemState GetItemState();
}
