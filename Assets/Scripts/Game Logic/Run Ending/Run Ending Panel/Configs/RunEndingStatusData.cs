using System;
using UnityEngine;
using UnityEngine.Localization;

namespace GameLogic
{
    [Serializable]
    public class RunEndingStatusData
    {
        [SerializeField] private RunEndingStatus _status;
        [SerializeField] private Sprite _statusIcon;
        [SerializeField] private LocalizedString _statusDescription;

        public RunEndingStatus Status => _status;
        public Sprite StatusIcon => _statusIcon;
        public LocalizedString StatusDescription => _statusDescription;
    }
}