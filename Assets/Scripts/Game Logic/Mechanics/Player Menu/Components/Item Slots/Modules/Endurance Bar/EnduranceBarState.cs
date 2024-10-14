using System;
using UnityEngine;

namespace GameLogic.PlayerMenu
{
    [Serializable]
    public class EnduranceBarState
    {
        [SerializeField] private Color _color;
        [SerializeField] private int _minEndurancePercentage;
        [SerializeField] private int _maxEndurancePercentage;

        public Color Color => _color;
        public float MinEndurancePercentage => _minEndurancePercentage;
        public float MaxEndurancePercentage => _maxEndurancePercentage;
    }
}