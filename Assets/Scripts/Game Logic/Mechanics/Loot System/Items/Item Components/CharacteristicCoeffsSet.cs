using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "New Characteristic Coeffs Set", menuName = "Game Entities/Characteristic Coeffs Set", order = 51)]
    public class CharacteristicCoeffsSet : ScriptableObject
    {
        [SerializeField]
        protected float damageCoefficient;
        [SerializeField]
        protected float durationCoefficient;
        [SerializeField]
        protected float attackSpeedCoefficient;
        [SerializeField]
        protected int maximumTemplateAdditionalCoefficient;
        [SerializeField]
        protected float blockingEfficiencyCoefficient;
        [SerializeField]
        protected float penetratingPowerCoefficient;
        [SerializeField]
        protected float energyCapacityCoefficient;

        public float DamageCoefficient => damageCoefficient;
        public float DurationCoefficient => durationCoefficient;
        public float AttackSpeedCoefficient => attackSpeedCoefficient;
        public int MaximumTemplateAdditionalCoefficient => maximumTemplateAdditionalCoefficient;
        public float BlockingEfficiencyCoefficient => blockingEfficiencyCoefficient;
        public float PenetratingPowerCoefficient => penetratingPowerCoefficient;
        public float EnergyCapacityCoefficient => energyCapacityCoefficient;
    }
}