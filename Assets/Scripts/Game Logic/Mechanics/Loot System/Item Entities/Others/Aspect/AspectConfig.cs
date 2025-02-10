using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "New Aspect", menuName = "Game Configs/Items/Aspect")]
    public class AspectConfig : ScriptableObject
    {
        [Space]
        [SerializeField] private int _level;
        [SerializeField] private ParticlesVFXType _particlesVFXType;
        [SerializeField] private AspectConfig[] _containedAspects;
        [SerializeField] private int _containedEnergyCount;

        public int Level => _level;
        public ParticlesVFXType ParticlesVFXType => _particlesVFXType;
        public AspectConfig[] ContainedAspects => _containedAspects;
        public int ContainedEnergyCount => _containedEnergyCount;

        public Aspect GetAspectState() => new();
    }
}