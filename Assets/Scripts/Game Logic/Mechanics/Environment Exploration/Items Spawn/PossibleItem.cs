using System;
using GameLogic.LootSystem;
using UnityEngine;

namespace GameLogic.EnvironmentExploration
{
    [Serializable]
    public class PossibleItem
    {
        [SerializeField] private ItemConfig _item;
        [Range(0, 100)]
        [SerializeField] private float _spawnChance;
        [SerializeField] private int _minCount;
        [SerializeField] private int _maxCount;

        public ItemConfig Item => _item;
        public float SpawnChance => _spawnChance;
        public int MinCount => _minCount;
        public int MaxCount => _maxCount;
    }
}