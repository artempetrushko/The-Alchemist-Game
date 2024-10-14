using UnityEngine;

namespace GameLogic.LootSystem
{
    [CreateAssetMenu(fileName = "New Characteristic Coeffs Set", menuName = "Game Configs/Items/Characteristic Coeffs Set")]
    public class CharacteristicCoeffsSet : ScriptableObject
    {
        [SerializeField] private float _damageCoefficient;
        [SerializeField] private float _durationCoefficient;
        [SerializeField] private float _attackSpeedCoefficient;
        [SerializeField] private int _maximumTemplateAdditionalCoefficient;
        [SerializeField] private float _blockingEfficiencyCoefficient;
        [SerializeField] private float _penetratingPowerCoefficient;
        [SerializeField] private float _energyCapacityCoefficient;

        public float DamageCoefficient => _damageCoefficient;
        public float DurationCoefficient => _durationCoefficient;
        public float AttackSpeedCoefficient => _attackSpeedCoefficient;
        public int MaximumTemplateAdditionalCoefficient => _maximumTemplateAdditionalCoefficient;
        public float BlockingEfficiencyCoefficient => _blockingEfficiencyCoefficient;
        public float PenetratingPowerCoefficient => _penetratingPowerCoefficient;
        public float EnergyCapacityCoefficient => _energyCapacityCoefficient;
    }
}