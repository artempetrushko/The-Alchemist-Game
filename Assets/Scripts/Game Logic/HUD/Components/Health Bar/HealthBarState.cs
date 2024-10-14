using System;
using UnityEngine;

namespace GameLogic
{
    [Serializable]
    public class HealthBarState
    {
        [SerializeField] private int _minHealthPercentage;
        [SerializeField] private int _maxHealthPercentage;
        [Space]
        [SerializeField] private Sprite _healthBarSprite;
        [SerializeField] private Sprite _healthBarFillingAreaSprite;

        public int MinHealthPercentage => _minHealthPercentage;
        public int MaxHealthPercentage => _maxHealthPercentage;
        public Sprite HealthBarSprite => _healthBarSprite;
        public Sprite HealthBarFillingAreaSprite => _healthBarFillingAreaSprite;
    }
}