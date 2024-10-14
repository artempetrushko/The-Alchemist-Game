using System;
using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic.PlayerMenu.Craft
{
	[Serializable]
    public class CraftingAvailabilityStatusTextsConfig
    {
        [SerializeField] private CraftingAvailabilityStatus _status;
        [SerializeField] private LocalizedString _statusText;

        public CraftingAvailabilityStatus Status => _status;
        public LocalizedString Text => _statusText;
    }
}