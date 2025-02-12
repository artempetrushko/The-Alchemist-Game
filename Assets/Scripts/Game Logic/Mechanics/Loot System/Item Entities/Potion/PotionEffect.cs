using GameLogic.Player;
using UnityEngine;

namespace GameLogic.LootSystem
{
    public abstract class PotionEffect : ScriptableObject
    {
        public abstract void Apply();
    }
}