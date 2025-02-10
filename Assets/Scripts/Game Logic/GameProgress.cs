using System;
using System.Collections.Generic;
using GameLogic.LootSystem;

namespace GameLogic
{
    [Serializable]
    public class GameProgress
    {
        public bool PlayerFinishedLevelEver;
        public bool PlayerFinishedLevelThrowStabilizedPortal;
        public bool StabilizerCreatedAndAvailable;
        public int StabilizerPartsCount;
        public List<Item> QuestItems = new();
    }
}