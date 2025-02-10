using System;

namespace GameLogic
{
    [Serializable]
    public class GameProgress
    {
        public bool PlayerFinishedLevelEver;
        public bool StabilizerCreatedAndAvailable;
        public int StabilizerPartsCount;
    }
}