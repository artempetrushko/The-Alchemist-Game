using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticlesVFXType
{
    Fireball,
    Bullet
}

[CreateAssetMenu(fileName = "New Aspect", menuName = "Game Entities/Items/Aspect", order = 51)]
public class AspectData : ScriptableObject
{
    [Space, SerializeField]
    private int level;
    [SerializeField]
    private ParticlesVFXType particlesVFXType;
    [SerializeField]
    private AspectData[] containedAspects;
    [SerializeField]
    private int containedEnergyCount;

    public int Level => level;
    public ParticlesVFXType ParticlesVFXType => particlesVFXType;
    public AspectData[] ContainedAspects => containedAspects;
    public int ContainedEnergyCount => containedEnergyCount;

    public AspectState GetAspectState() => new();
}
