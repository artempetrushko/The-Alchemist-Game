using System;
using UnityEngine;

namespace GameLogic.RunEnding
{
    [Serializable]
    public class RunEndingStatusData
    {
        [SerializeField]
        private RunEndingStatus status;
        [SerializeField]
        private Sprite statusIcon;
        [SerializeField]
        private string statusDescription;

        public RunEndingStatus Status => status;
        public Sprite StatusIcon => statusIcon;
        public string StatusDescription => statusDescription;
    }
}